using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Riesgos.Simefin.Domain.Entities;

namespace Riesgos.Simefin.Infrastructure.Oracle.Helpers
{

    public class OracleConnectionHelper
    {
        public static IConfiguration _configuration;

        public static void Initialize(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public static OracleConnection GetConnection()
        {
            string environmentVariable = _configuration!.GetSection("EnvironmentVariable").Value!;
            string environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable)!;
            if (!string.IsNullOrEmpty(environmentVariableValue))
            {
                var data = JsonConvert.DeserializeObject<EnvironmentVariables>(environmentVariableValue!);
                string connectionString = _configuration!.GetSection("ConnectionStrings").GetSection("OracleConexion").Value!;
                string valueConnectionString = string.Format(connectionString, data!.ServerNameOrIP, data.Port, data.Scheme, data.UserId, data.Password);
                var connection = new OracleConnection(valueConnectionString);

                return connection;
            }

            return new OracleConnection();
        }


    }

}
