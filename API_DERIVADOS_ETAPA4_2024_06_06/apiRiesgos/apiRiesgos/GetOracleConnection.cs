using Oracle.ManagedDataAccess.Client;

namespace apiRiesgos
{
    public class GetOracleConnection
    {
        IConfiguration configuration;

        public GetOracleConnection(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public OracleConnection GetConnection(string source)
        {

            //var connectionString = new Utilerias().DesEncriptarPass(configuration.GetSection("ConnectionStrings").GetSection(source).Value);
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection(source).Value;

            var conn = new OracleConnection(connectionString);

            return conn;
        }

        public OracleConnection GetConnectionDerivados(string source)
        {

            //var connectionString = new Utilerias().DesEncriptarPass(configuration.GetSection("ConnectionStrings").GetSection(source).Value);
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection(source).Value;

            var conn = new OracleConnection(connectionString);

            return conn;
        }
    }
}
