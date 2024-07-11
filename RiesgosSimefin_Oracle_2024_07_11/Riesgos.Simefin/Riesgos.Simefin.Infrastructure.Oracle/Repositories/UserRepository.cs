using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Application.Interfaces.User;
using Riesgos.Simefin.Domain.Entities;
using Riesgos.Simefin.Infrastructure.Oracle.Helpers;

namespace Riesgos.Simefin.Infrastructure.Oracle.Repositories
{

    /// <summary>
    /// Clase que implementa los métodos de acceso a datos para Usuarios
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _configuration;
        private readonly string? _usersTableName = string.Empty;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _usersTableName = _configuration.GetSection("DataBaseTables").GetSection("T_Usuarios").Value!;
        }

        /// <summary>
        /// Agregar nuevo usuario
        /// </summary>
        /// <param name="entity">Contenedor con información del usuario</param>
        /// <returns></returns>
        public async Task<int> AddAsync(Usuario entity)
        {
            using (var connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    //p_Id = entity.Id,
                    p_UserName = entity.UserName,
                    p_Nombre = entity.Nombre,
                    p_Password = entity.Password,
                    p_Rol = entity.Rol
                };
                var query = @$"INSERT INTO {_usersTableName} (UserName, Nombre, Password, Rol) 
                            VALUES (:p_UserName, :p_Nombre, :p_Password, :p_Rol)";
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                return rowsAffected;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtener información de todos los usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using (OracleConnection connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                string strQuery = @$"SELECT Id, UserName, Nombre, Password, Rol FROM {_usersTableName}";
                return await connection.QueryAsync<Usuario>(strQuery);
            }
        }

        /// <summary>
        /// Obtener información de un usuario
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        public async Task<Usuario> GetByIdAsync(int userId)
        {
            using (OracleConnection connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                var parameters = new { p_Id = userId };
                string strQuery = @$"SELECT Id, UserName, Nombre, Password, Rol FROM {_usersTableName} WHERE Id = :p_Id";
                var user = await connection.QueryFirstOrDefaultAsync<Usuario>(strQuery, parameters);

                return user!;
            }
        }

        public Task<bool> UpdateAsync(Usuario entity)
        {
            throw new NotImplementedException();

        }
    }

}
