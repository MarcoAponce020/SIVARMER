using AutoMapper.Execution;
using InterfazRiesgosSimefin_API.DAO;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace InterfazRiesgosSimefin_API.Repository
{
    public class PortafolioRepository : Repository<Portafolio>, IPortafolioRepository
    {
        private readonly ApplicationDbContext _db;
        private const int rowDate = 2;

        public PortafolioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Portafolio> Actualizar(Portafolio entidad)
        {
            entidad.FechaModificacion = DateTime.Now;
            _db.Portafolios.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;

        }



        #region Carga de Excel ...

        public async Task<APIResponse> ExcelLoad_OLD(IFormFile file)
        {
            var response = new APIResponse();
            var resultReader = string.Empty;
            string worksheetsName = "Hoja1";
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string? fechaPosicionPivote = string.Empty;
            List<Portafolio> portafoliosList = new List<Portafolio>();

            List<string?> listaFechas = new List<string?>();
            List<string> listaDatos = new List<string>();

            try
            {
                //Obteniendo el catálogo de SubPortafolios
                var subPortafolioList = this.GetSubPortfolios();
                if (!subPortafolioList.Any() && subPortafolioList.Count() == 0)
                {
                    response.Mensaje = "Faltan datos para procesar -Catálogo de SubPortafolios-.";
                    response.IsExitoso = false;
                    response.statusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                ExcelWorksheet? worksheet = null;
                using (var reader = new System.IO.StreamReader(file.OpenReadStream()))
                using (ExcelPackage package = new ExcelPackage())
                {
                    resultReader = reader.ReadToEnd();
                    worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet = package.Workbook.Worksheets[0];
                    worksheet.Cells["A1"].LoadFromText(resultReader, format);

                    //Validando la fecha pivote
                    fechaPosicionPivote = worksheet.Cells[1, 1].Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(fechaPosicionPivote))
                    {
                        response.Mensaje = "Faltan datos para procesar -fecha de posición inicial-.";
                        response.IsExitoso = false;
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    //Obteniendo el listado de fechas
                    listaFechas = this.GetDateList_OLD(worksheet);
                    if (!listaFechas.Any() && listaFechas.Count == 0)
                    {
                        response.Mensaje = "Faltan datos para procesar -fechas de portafolios-.";
                        response.IsExitoso = false;
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    Portafolio newPortafolio = new Portafolio();
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    //Partiendo de la fila 3, donde se encuentra el cuerpo de los portafolios
                    for (int row = 3; row <= rowCount; row++)
                    {
                        listaDatos = new List<string>();
                        newPortafolio = new Portafolio
                        {
                            F_Posicion = fechaPosicionPivote,
                            NombrePortafolio = "TOTAL",
                            No_Envio = 1,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        for (int col = 1; col <= colCount; col++)
                        {
                            string? currentCellValue = worksheet.Cells[row, col].Value?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(currentCellValue))
                            {
                                //Subportafolios
                                if (col == 1)
                                {
                                    string? subPortfolio = currentCellValue.ToUpper();
                                    newPortafolio.SubPortafolio = subPortfolio;
                                    newPortafolio.SubPortafolioId = this.GetSubPortfolioValue(subPortafolioList, newPortafolio.SubPortafolio);
                                    //Dado el caso de que el SubPortafolioId no existe en BD
                                    if (newPortafolio.SubPortafolioId == 0)
                                    {
                                        response.ErrorMessages.Add($"Configuración errónea en archivo origen. {Environment.NewLine} El valor de SubPortafolio no existe en BD.. Fila ({row}, {col}) => '{subPortfolio}'.");
                                    }
                                }

                                if (col > 1)
                                {
                                    if (subPortafolioList.Any(x => x.SubPortafolioId == newPortafolio.SubPortafolioId))
                                    {
                                        listaDatos.Add(currentCellValue);
                                    }
                                }
                            }
                            else
                            {
                                //No se inserta por falta de fecha
                                if (row > 1 && row == row - 1)
                                {
                                    response.ErrorMessages.Add($"Fila ({row}, {col}), no contiene el valor de fecha.");
                                }
                            }
                        } //col

                        if (newPortafolio != null && newPortafolio.SubPortafolioId > 0)
                        {
                            if (listaFechas.Count() != listaDatos.Count())
                            {
                                response.Mensaje = "Archivo no procesado por incoherencia en datos.";
                                response.ErrorMessages.Add($"Fila ({row}). La lista de fechas no coincide con la lista de datos.");
                                response.IsExitoso = false;
                                response.statusCode = HttpStatusCode.BadRequest;
                                return response;
                            }

                            if (listaFechas.Count() > 0)
                            {
                                //newPortafolio.ListaFechas = string.Join(",", listaFechas);
                            }
                            if (listaDatos.Count() > 0)
                            {
                                newPortafolio.listaDatos = string.Join(",", listaDatos);
                            }
                            //Agregando el nuevo porfatolio a la lista
                            portafoliosList.Add(newPortafolio);
                        }
                    } //row

                    var jsonString = JsonSerializer.Serialize(portafoliosList);

                    var result = await this.SavePortfolios(portafoliosList);
                    response.ErrorMessages.AddRange(result);
                }
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ex.Message.ToString());
                response.statusCode = HttpStatusCode.BadRequest;
                response.Resultado = false;
                return response;
            }

            response.Resultado = true;
            response.statusCode = HttpStatusCode.OK;
            response.Mensaje = "Archivo procesado con éxito.";
            return response;
        }

        public async Task<APIResponse> ExcelLoad(IFormFile file)
        {
            var response = new APIResponse();
            var resultReader = string.Empty;
            string worksheetsName = "Hoja1";
            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.TextQualifier = '"';
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            string? fechaPosicionPivote = string.Empty;
            List<Portafolio> portafoliosList = new List<Portafolio>();

            Dictionary<int, string?> dictionaryDates = new Dictionary<int, string?>();
            Dictionary<int, string?> dictionaryData = new Dictionary<int, string?>();
            List<string> listaDatos = new List<string>();

            try
            {
                //Obteniendo el catálogo de SubPortafolios
                var subPortafolioList = this.GetSubPortfolios();
                if (!subPortafolioList.Any() && subPortafolioList.Count() == 0)
                {
                    response.Mensaje = "Faltan datos para procesar -Catálogo de SubPortafolios-.";
                    response.IsExitoso = false;
                    response.statusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                ExcelWorksheet? worksheet = null;
                using (var reader = new System.IO.StreamReader(file.OpenReadStream()))
                using (ExcelPackage package = new ExcelPackage())
                {
                    resultReader = reader.ReadToEnd();
                    worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                    worksheet = package.Workbook.Worksheets[0];
                    worksheet.Cells["A1"].LoadFromText(resultReader, format);

                    //Validando la fecha pivote
                    fechaPosicionPivote = worksheet.Cells[1, 1].Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(fechaPosicionPivote))
                    {
                        response.Mensaje = "Faltan datos para procesar -fecha de posición inicial-.";
                        response.IsExitoso = false;
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    //Obteniendo el listado de fechas
                    dictionaryDates = this.GetDateList(worksheet);
                    if (!dictionaryDates.Any() && dictionaryDates.Count == 0)
                    {
                        response.Mensaje = "Faltan datos para procesar -fechas de portafolios-.";
                        response.IsExitoso = false;
                        response.statusCode = HttpStatusCode.BadRequest;
                        return response;
                    }

                    Portafolio newPortafolio = new Portafolio();
                    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    int rowCount = worksheet.Dimension.End.Row;     //get row count

                    //Partiendo de la fila 3, donde se encuentra el cuerpo de los portafolios
                    for (int row = 3; row <= rowCount; row++)
                    {
                        dictionaryData = new Dictionary<int, string?>();
                        newPortafolio = new Portafolio
                        {
                            F_Posicion = fechaPosicionPivote,
                            NombrePortafolio = "TOTAL",
                            No_Envio = 1,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        for (int col = 1; col <= colCount; col++)
                        {
                            string? currentCellValue = worksheet.Cells[row, col].Value?.ToString()?.Trim();
                            if (!string.IsNullOrEmpty(currentCellValue))
                            {
                                //Subportafolios
                                if (col == 1)
                                {
                                    string? subPortfolio = currentCellValue.ToUpper();
                                    newPortafolio.SubPortafolio = subPortfolio;
                                    newPortafolio.SubPortafolioId = this.GetSubPortfolioValue(subPortafolioList, newPortafolio.SubPortafolio);
                                    //Dado el caso de que el SubPortafolioId no existe en BD
                                    if (newPortafolio.SubPortafolioId == 0)
                                    {
                                        response.ErrorMessages.Add($"Configuración errónea en archivo origen. {Environment.NewLine} El valor de SubPortafolio no existe en BD.. Fila ({row}, {col}) => '{subPortfolio}'.");
                                    }
                                }

                                if (col > 1)
                                {
                                    if (subPortafolioList.Any(x => x.SubPortafolioId == newPortafolio.SubPortafolioId))
                                    {
                                        dictionaryData.Add(col, currentCellValue);
                                    }
                                }
                            }
                        } //col

                        if (newPortafolio != null && newPortafolio.SubPortafolioId > 0)
                        {
                            //Comparando fechas vs datos. En cada diccionario se agregó como Key la columna.
                            //Entonces se compara que las claves de cada diccionario existan en ambos. Si hay diferencia, abortar
                            bool isKeysTheSame = dictionaryDates.Count == dictionaryData.Count && !dictionaryDates.Keys.Except(dictionaryData.Keys).Any();
                            if (!isKeysTheSame)
                            {
                                response.Mensaje = "Archivo no procesado por incoherencia en datos.";
                                response.ErrorMessages.Add($"Fila ({row}). La lista de fechas no coincide con la lista de datos.");
                                response.IsExitoso = false;
                                response.statusCode = HttpStatusCode.BadRequest;
                                return response;
                            }

                            ////Comparando fechas vs datos
                            //if (dictionaryDates.Count() != dictionaryData.Count())
                            //{
                            //    response.Mensaje = "Archivo no procesado por incoherencia en datos.";
                            //    response.ErrorMessages.Add($"Fila ({row}). La lista de fechas no coincide con la lista de datos.");
                            //    response.IsExitoso = false;
                            //    response.statusCode = HttpStatusCode.BadRequest;
                            //    return response;
                            //}

                            var datos = (from f in dictionaryDates
                                         join d in dictionaryData on f.Key equals d.Key
                                         select new { Fecha = f.Value, Valor = d.Value }).ToList();

                            if (datos != null)
                            {
                                newPortafolio.listaDatos = JsonSerializer.Serialize(datos);
                            }

                            //Agregando el nuevo porfatolio a la lista
                            portafoliosList.Add(newPortafolio);
                        }
                    } //row

                    var jsonString = JsonSerializer.Serialize(portafoliosList);

                    var result = await this.SavePortfolios(portafoliosList);
                    response.ErrorMessages.AddRange(result);
                }
            }
            catch (KeyNotFoundException ke)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add($"Datos corruptos. { ke.Message.ToString() }");
                response.statusCode = HttpStatusCode.BadRequest;
                response.Resultado = false;
                return response;
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.ErrorMessages.Add(ex.Message.ToString());
                response.statusCode = HttpStatusCode.BadRequest;
                response.Resultado = false;
                return response;
            }

            response.Resultado = true;
            response.statusCode = HttpStatusCode.OK;
            response.Mensaje = "Archivo procesado con éxito.";
            return response;
        }

        /// <summary>
        /// Get date list
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private Dictionary<int, string?> GetDateList(ExcelWorksheet worksheet)
        {
            Dictionary<int, string?> dateList = new Dictionary<int, string?>();
            int colCount = worksheet.Dimension.End.Column;

            for (int col = 2; col <= colCount; col++)
            {
                string? currentCellValue = worksheet.Cells[rowDate, col].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(currentCellValue))
                {
                    dateList.Add(col, currentCellValue);
                }
            }

            return dateList;
        }


        private List<string?> GetDateList_OLD(ExcelWorksheet worksheet) 
        {
            List<string?> dateList = new List<string?>();
            int colCount = worksheet.Dimension.End.Column;
            int row = 2;

            for (int col = 2; col <= colCount; col++)
            {
                string? currentCellValue = worksheet.Cells[row, col].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(currentCellValue))
                {
                    dateList.Add(currentCellValue);
                }
            }

            return dateList;
        }

        /// <summary>
        /// Guardando en BD la lista de portafolios suministrada
        /// </summary>
        /// <param name="portafoliosList">Listado de portafolios</param>
        /// <returns></returns>
        private async Task<List<string>> SavePortfolios(List<Portafolio> portafoliosList)
        {
            List<string> response = new List<string>();

            if (portafoliosList.Any())
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        //Validar la existencia de portafolio
                        foreach (var item in portafoliosList)
                        {
                            var portafolio = await _db.Portafolios.AsNoTracking().FirstOrDefaultAsync(x => x.F_Posicion == item.F_Posicion && x.SubPortafolioId == item.SubPortafolioId);
                            if (portafolio != null)
                            {
                                //Actualizar
                                portafolio.FechaModificacion = DateTime.Now;
                                portafolio.No_Envio += 1;
                                portafolio.listaDatos = item.listaDatos;
                                _db.Entry(portafolio).State = EntityState.Modified;
                            }
                            else
                            {
                                _db.Portafolios.Add(item);
                            }
                            _db.SaveChanges();
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
            return response;
        }

        /// <summary>
        /// Obtener el catálogo de los SubPortafolios
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SubPortafolios> GetSubPortfolios()
        {
            var response = _db.SubPortafolios.AsNoTracking().ToList();
            return response;
        }

        /// <summary>
        /// Obtener el identificador del SubPortafolio
        /// </summary>
        /// <param name="subPortfolioList">Catálogo de SubPortafolios</param>
        /// <param name="value">SubPortafolio proveniente del Excel</param>
        /// <returns></returns>
        private int GetSubPortfolioValue(IEnumerable<SubPortafolios> subPortfolioList, string? value)
        {
            int id = 0;

            if (!string.IsNullOrEmpty(value))
            {
                id = subPortfolioList.Where(x => x.Descripcion?.Trim().ToUpper() == value.Trim().ToUpper()).Select(y => y.SubPortafolioId).FirstOrDefault();
            }

            return id;
        }


        #endregion


    }
}
