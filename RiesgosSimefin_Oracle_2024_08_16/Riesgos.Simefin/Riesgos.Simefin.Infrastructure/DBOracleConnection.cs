using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Riesgos.Simefin.Infrastructure
{

    public class DBOracleConnection
    {

        private static OracleConnection _connection;
        private static IConfiguration configuration;

        public static OracleConnection GetOracleConnection { 
            get {
                if (_connection == null)
                {
                    _connection = GetConnection();
                }
                return _connection; 
            } 
        }

        private DBOracleConnection(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        private static OracleConnection GetConnection() 
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("OracleConexion").Value;

            var connection = new OracleConnection(connectionString);

            return connection;
        }

    }

}
