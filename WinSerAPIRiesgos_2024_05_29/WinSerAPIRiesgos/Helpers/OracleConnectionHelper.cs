using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace WinSerAPIRiesgos.Helpers
{

    public class OracleConnectionHelper
    {

        public static OracleConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
            var connection = new OracleConnection(connectionString);

            return connection;
        }


    }

}

