using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Application.Interfaces.Authorization;
using Riesgos.Simefin.Domain.Entities;
using Riesgos.Simefin.Infrastructure.Oracle.Helpers;

namespace Riesgos.Simefin.Infrastructure.Oracle.Repositories
{

    /// <summary>
    /// Clase que implementa los métodos de acceso a datos para Tokens
    /// </summary>
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly IConfiguration _configuration;
        private readonly string? _tokensTableName = string.Empty;

        public AuthorizationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokensTableName = _configuration.GetSection("DataBaseTables").GetSection("T_Tokens").Value!;
        }

        /// <summary>
        /// Agrega un nuevo Token
        /// </summary>
        /// <param name="entity">Contenedor con información del Token</param>
        /// <returns></returns>
        public async Task<int> AddAsync(Token entity)
        {
            using (var connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    p_AccessToken = entity.AccessToken,
                    p_ExpirationDate = entity.ExpirationDate,
                    //p_IsValid = entity.IsValid, //Es calculada
                    p_RefreshToken = entity.RefreshToken,
                    //p_TokenId = entity.TokenId, //Se genera con la secuencia
                    p_UsuarioId = entity.UsuarioId,
                };
                var query = @$"INSERT INTO {_tokensTableName} (UsuarioId, AccessToken, RefreshToken, ExpirationDate) 
                            VALUES (:p_UsuarioId, :p_AccessToken, :p_RefreshToken, :p_ExpirationDate)";
                var rowsAffected = await connection.ExecuteAsync(query, parameters);
                return rowsAffected;
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtener todos los Tokens
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> GetAllAsync()
        {
            using (var connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                string strQuery = @$"SELECT TokenId, UsuarioId, AccessToken, RefreshToken, ExpirationDate, IsValid 
                                     FROM {_tokensTableName}";
                return await connection.QueryAsync<Token>(strQuery);
            }
        }

        /// <summary>
        /// Obtener un Token a través de un identificador
        /// </summary>
        /// <param name="tokenId">Identificador</param>
        /// <returns></returns>
        public async Task<Token> GetByIdAsync(int tokenId)
        {
            using (OracleConnection connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                var parameters = new { p_Id = tokenId };
                string strQuery = @$"SELECT TokenId, UsuarioId, AccessToken, RefreshToken, ExpirationDate, IsValid 
                                     FROM {_tokensTableName} WHERE TokenId = :p_Id";
                var user = await connection.QueryFirstOrDefaultAsync<Token>(strQuery, parameters);

                return user!;
            }
        }

        /// <summary>
        /// Actualizando información de un Token
        /// </summary>
        /// <param name="entity">Contenedor con información de Token</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Token entity)
        {
            using (var connection = OracleConnectionHelper.GetConnection())
            {
                await connection.OpenAsync();
                var parameters = new
                {
                    p_AccessToken = entity.AccessToken,
                    p_ExpirationDate = entity.ExpirationDate,
                    p_RefreshToken = entity.RefreshToken,
                    p_TokenId = entity.TokenId,
                    p_UsuarioId = entity.UsuarioId
                };
                var query = @$"UPDATE {_tokensTableName} 
                            SET AccessToken = :p_AccessToken,
                                ExpirationDate = :p_ExpirationDate,
                                RefreshToken = :p_RefreshToken,
                                UsuarioId = :p_UsuarioId
                                WHERE TokenId = :p_TokenId";
                var affectedRows = await connection.ExecuteAsync(query, parameters);
                return affectedRows > 0;
            }
        }
    }

}
