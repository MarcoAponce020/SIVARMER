using ENTITY;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

namespace apiRiesgos
{
    public class GetOracleConnection
    {
        private readonly IConfiguration _configuration;

        public GetOracleConnection(IConfiguration configuration) => _configuration = configuration;

        public OracleConnection GetConnection(string source)
        {
            string environmentVariable = string.Empty;
            switch (source)
            {
                case EnumDataBaseTypes.DERIVADOS:
                    environmentVariable = _configuration!.GetSection("EnvironmentVariableDER").Value!;
                    break;
                case EnumDataBaseTypes.VARMER:
                    environmentVariable = _configuration!.GetSection("EnvironmentVariableVAR").Value!;
                    break;
            }
                
            string environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable)!;
            if (!string.IsNullOrEmpty(environmentVariableValue))
            {
                var data = JsonConvert.DeserializeObject<DataBaseEnvironmentVariables>(environmentVariableValue!);
                string connectionString = _configuration!.GetSection("ConnectionStrings").GetSection("OracleConexion").Value!;
                string valueConnectionString = string.Format(connectionString, data!.ServerNameOrIP, data.Port, data.Scheme, data.UserId, data.Password);
                var connection = new OracleConnection(valueConnectionString);

                return connection;
            }

            return new OracleConnection();
        }

    }
}
