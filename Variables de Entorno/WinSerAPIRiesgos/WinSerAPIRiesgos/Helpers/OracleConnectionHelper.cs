using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using WinSerAPIRiesgos.Models;

namespace WinSerAPIRiesgos.Helpers
{

    public class OracleConnectionHelper
    {

        public static OracleConnection GetConnection()
        {
            string environmentVariable = ConfigurationManager.AppSettings.Get("EnvironmentVariable");
            string environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable);
            if (!string.IsNullOrEmpty(environmentVariableValue))
            {
                var data = JsonConvert.DeserializeObject<EnvironmentVariables>(environmentVariableValue);
                string connectionString = ConfigurationManager.ConnectionStrings["OracleConexion"].ConnectionString;
                string valueConnectionString = string.Format(connectionString, data.DataBase.ServerNameOrIP, data.DataBase.Port, data.DataBase.Scheme, data.DataBase.UserId, data.DataBase.Password);
                var connection = new OracleConnection(valueConnectionString);

                return connection;
            }

            return new OracleConnection();
        }


    }

}

