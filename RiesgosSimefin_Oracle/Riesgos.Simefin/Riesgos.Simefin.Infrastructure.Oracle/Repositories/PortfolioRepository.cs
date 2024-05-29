using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Application.DTOs.Portfolio;
using Riesgos.Simefin.Application.Interfaces.Portfolio;
using Riesgos.Simefin.Domain.Entities;
using System.Data;

namespace Riesgos.Simefin.Infrastructure.Oracle.Repositories
{

    /// <summary>
    /// Clase que implementa los métodos de acceso a datos para Portafolios
    /// </summary>
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        //private readonly IMapper _mapper;
        private readonly string _portfolioTableName = string.Empty;
        private readonly string _subPortfolioTableName = string.Empty;

        public PortfolioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //_mapper = mapper;
            _connectionString = _configuration.GetSection("ConnectionStrings").GetSection("OracleConexion").Value!;
            _portfolioTableName = _configuration.GetSection("DataBaseTables").GetSection("T_Portafolios").Value!;
            _subPortfolioTableName = _configuration.GetSection("DataBaseTables").GetSection("T_SubPortafolios").Value!;
        }

        /// <summary>
        /// Obtener todos los Portafolios
        /// </summary>
        /// <param name="whereFilter">Filtros sobre la consulta</param>
        /// <returns></returns>
        public async Task<IEnumerable<Portafolio>> GetAllAsync(List<object>? filters = null)
        {
            using (IDbConnection connection = new OracleConnection(_connectionString))
            {
                //await connection.OpenAsync();
                connection.Open();
                string strQuery = string.Empty;
                string strWhere = string.Empty;
                int countParameters = 0;
                var filterList = new Dictionary<string, object>() { };
                if (filters != null)
                {
                    strWhere = " WHERE ";
                    foreach (var item in filters!)
                    {
                        countParameters += 1;
                        var props = item.GetType().GetProperties();
                        var pairs = props.Select(x => x.Name + "=" + x.GetValue(item, null)).ToArray();
                        var pairDictionary = props.ToDictionary(x => x.Name, x => x.GetValue(item, null)?.ToString());
                        var columName = pairDictionary.Keys.Select(x => x).FirstOrDefault();
                        var parameterName = $"p_{columName}";
                        if (countParameters > 1) // && countParameters <= filters.Count
                        { 
                            strWhere += " AND " ;
                        }
                        strWhere += $"A.{columName} = :{parameterName}";
                    }
                    filterList = this.GetAnonymusParameters(filters);
                    strQuery = @$"SELECT A.IdPortafolio, A.F_Posicion, A.NombrePortafolio, A.ListaDatos, A.No_Envio, A.FechaCreacion, A.FechaModificacion, A.SubPortafolioId, B.Descripcion SubPortafolio
                                FROM {_portfolioTableName} A INNER JOIN {_subPortfolioTableName} B ON B.SubPortafolioId = A.SubPortafolioId";
                    strQuery += strWhere;
                }
                else
                {
                    strQuery = @$"SELECT A.IdPortafolio, A.F_Posicion, A.NombrePortafolio, A.ListaDatos, A.No_Envio, A.FechaCreacion, A.FechaModificacion, A.SubPortafolioId, B.Descripcion SubPortafolio
                                FROM {_portfolioTableName} A INNER JOIN {_subPortfolioTableName} B ON B.SubPortafolioId = A.SubPortafolioId";
                }
                var portfolios = await connection.QueryAsync<Portafolio>(strQuery, filterList);
                portfolios.ToList().ForEach(portfolio => {
                    if (portfolio != null && portfolio.ListaDatos != null)
                    {
                        portfolio!.ListaDatos = JsonConvert.DeserializeObject<List<ListaDatosDTO>>(portfolio.ListaDatos!);
                    }
                });
                return portfolios;
            }
        }

        /// <summary>
        /// Obtener portafolio por Identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public async Task<Portafolio> GetByIdAsync(int id)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new { p_Id = id };
                string strQuery = @$"SELECT A.IdPortafolio, A.F_Posicion, A.NombrePortafolio, A.ListaDatos, A.No_Envio, A.FechaCreacion, A.FechaModificacion, A.SubPortafolioId, B.Descripcion SubPortafolio
                                    FROM {_portfolioTableName} A INNER JOIN {_subPortfolioTableName} B ON B.SubPortafolioId = A.SubPortafolioId
                                    WHERE IdPortafolio = :p_Id";
                var portfolio = await connection.QueryFirstOrDefaultAsync<Portafolio>(strQuery, parameters);

                if (portfolio != null && portfolio.ListaDatos != null)
                {
                    portfolio!.ListaDatos = JsonConvert.DeserializeObject<List<ListaDatosDTO>>(portfolio.ListaDatos!);
                }

                return portfolio!;
            }
        }

        /// <summary>
        /// Agregar nuevo portafolio.
        /// </summary>
        /// <param name="entity">Contenedor con información del portafolio.</param>
        /// <returns></returns>
        public async Task<int> AddAsync(Portafolio entity)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    //p_Id = entity.IdPortafolio,
                    p_FechaCreacion = DateTime.UtcNow,
                    p_FechaModificacion = DateTime.UtcNow,
                    p_F_Posicion = entity.F_Posicion!,
                    p_ListaDatos = entity.ListaDatos!,
                    p_NombrePortafolio = entity.NombrePortafolio!,
                    //p_No_Envio = entity.No_Envio,
                    p_No_Envio = 1,
                    //p_SubPortafolio = entity.SubPortafolio!,
                    p_SubPortafolioId = entity.SubPortafolioId
                };
                var query = @$"INSERT INTO {_portfolioTableName} (FechaCreacion, FechaModificacion, F_Posicion, ListaDatos, NombrePortafolio, No_Envio, SubPortafolioId) 
                            VALUES (:p_FechaCreacion, :p_FechaModificacion, :p_F_Posicion, :p_ListaDatos, :p_NombrePortafolio, :p_No_Envio, :p_SubPortafolioId)";
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                return rowsAffected;
            }
        }

        /// <summary>
        /// Actualización de un portafolio
        /// </summary>
        /// <param name="entity">Contenedor con información de Portafolio</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Portafolio entity)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    p_Id = entity.IdPortafolio,
                    p_FechaModificacion = DateTime.UtcNow,
                    p_ListaDatos = System.Text.Json.JsonSerializer.Serialize(entity.ListaDatos),
                    p_No_Envio = entity.No_Envio + 1
                };
                var query = @$"UPDATE {_portfolioTableName} 
                            SET FechaModificacion = :p_FechaModificacion,
                                ListaDatos = :p_ListaDatos,
                                No_Envio = :p_No_Envio
                            WHERE IdPortafolio = :p_Id";
                var affectedRows = await connection.ExecuteAsync(query, parameters);
                return affectedRows > 0;
            }
        }

        /// <summary>
        /// Eliminar un Portafolio
        /// </summary>
        /// <param name="id">Identificador de Portafolio</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                var parameters = new { p_Id = id };
                var affectedRows = await connection.ExecuteAsync($"DELETE FROM {_portfolioTableName} WHERE IdPortafolio = :p_Id", parameters);
                return affectedRows > 0;
            }
        }

        /// <summary>
        /// Obtener todos los SubPortafolios
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SubPortafolios>> GetAllSubPortfoliosAsync()
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                await connection.OpenAsync();
                var subPortfolios = await connection.QueryAsync<SubPortafolios>($"SELECT SubPortafolioId, Descripcion FROM {_subPortfolioTableName}");
                return subPortfolios;
            }
        }

        /// <summary>
        /// Guardando en BD la lista de portafolios suministrada
        /// </summary>
        /// <param name="portafoliosList">Listado de portafolios</param>
        /// <returns></returns>
        public async Task<List<string>> SavePortfoliosList(List<Portafolio> portafoliosList)
        {
            List<string> response = new List<string>();

            if (portafoliosList.Any())
            {
                using (var connection = new OracleConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //Validar la existencia de portafolio
                            foreach (var item in portafoliosList)
                            {
                                //Construir lista de parámetros por los cuales filtrar.
                                //El nombre del parámetro debe ser el nombre de la columna en BD.
                                List<object> filters = new List<object>() {
                                    new {
                                        F_Posicion = item.F_Posicion
                                    },
                                    new {
                                        SubPortafolioId = item.SubPortafolioId
                                    }
                                };

                                var portafolios = await this.GetAllAsync(filters);
                                if (portafolios.Any() && portafolios.Count() > 0)
                                {
                                    await this.UpdateAsync(portafolios.FirstOrDefault()!);
                                }
                                else
                                {
                                    await this.AddAsync(item);
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            response.Add(ex.Message.ToString());
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            return response;
        }



        /// <summary>
        /// Construir parámetros Oracle de una lista de objetos anónimos
        /// </summary>
        /// <param name="anonymousObjectsList"></param>
        /// <returns></returns>
        private List<OracleParameter> GetOracleParameters(IEnumerable<object> anonymousObjectsList)
        {
            List<OracleParameter> parameterList = new List<OracleParameter>();

            foreach (var anonymousObject in anonymousObjectsList)
            {
                // Usamos reflexión para obtener las propiedades del objeto anónimo
                var properties = anonymousObject.GetType().GetProperties();

                foreach (var propiedad in properties)
                {
                    // Obtenemos el nombre y el valor de la propiedad
                    string nombreParametro = ":p_" + propiedad.Name; // Oracle utiliza ':' como prefijo para los parámetros
                    object valorParametro = propiedad.GetValue(anonymousObject)!;

                    // Creamos el OracleParameter y lo añadimos a la lista
                    OracleParameter parametro = new OracleParameter(nombreParametro, valorParametro ?? DBNull.Value);
                    parameterList.Add(parametro);
                }
            }

            return parameterList;
        }

        /// <summary>
        /// Construir parámetros de una lista de objetos anónimos
        /// </summary>
        /// <param name="anonymousObjectsList"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetAnonymusParameters(IEnumerable<object> anonymousObjectsList)
        {
            var parameterList = new Dictionary<string, object>() { };

            foreach (var anonymousObject in anonymousObjectsList)
            {
                // Usamos reflexión para obtener las propiedades del objeto anónimo
                var properties = anonymousObject.GetType().GetProperties();

                foreach (var propiedad in properties)
                {
                    // Obtenemos el nombre y el valor de la propiedad
                    string nombreParametro = "p_" + propiedad.Name;
                    object valorParametro = propiedad.GetValue(anonymousObject)!;

                    // Creamos el parámetro y lo añadimos a la lista
                    parameterList.Add(nombreParametro, valorParametro!);
                }
            }

            return parameterList;
        }

    }
}
