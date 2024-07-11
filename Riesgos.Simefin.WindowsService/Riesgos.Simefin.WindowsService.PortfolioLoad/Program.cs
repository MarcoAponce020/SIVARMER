using Newtonsoft.Json;
using Riesgos.Simefin.WindowsService.PortfolioLoad.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Riesgos.Simefin.WindowsService.PortfolioLoad
{
    internal static class Program
    {

        //static async Task Main(string[] args)
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceExcelLoad()
            };

            // If you don’t want the exe to run as console when double-clicked, then use:
            //   if (System.Diagnostics.Debugger.IsAttached)
            if (Environment.UserInteractive)
            {
                Console.WriteLine("Service running, press '[Enter]' to stop.");
                ((ServiceExcelLoad)ServicesToRun[0]).Start(null);
                Console.ReadLine();
                ((ServiceExcelLoad)ServicesToRun[0]).Stop();
            }
            else
            {
                ServiceBase.Run(ServicesToRun);
            }

            #region Código pruebas locales

            //ApiResponse tokenResponse = await GetToken();
            //if (tokenResponse.IsExitoso)
            //{
            //    TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Resultado.ToString());
            //    Console.WriteLine($"Token obtenido => {token}");

            //    var result = await ExecuteLoadingProcess(token.AccessToken);

            //    Console.WriteLine("result => " + JsonConvert.SerializeObject(result));
            //}
            //else
            //{
            //    Console.WriteLine(JsonConvert.SerializeObject(tokenResponse));
            //}

            //Console.ReadLine();

            #endregion

        }

        #region Código pruebas locales

        private static async Task<ApiResponse> GetToken()
        {
            ApiResponse response = new ApiResponse { IsExitoso = false };

            string environmentVariable = ConfigurationManager.AppSettings["EnvironmentVariable"];
            string environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable);
            if (string.IsNullOrEmpty(environmentVariableValue))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Mensaje = "Variable de entorno no definida.";
                return response;
            }

            var userData = JsonConvert.DeserializeObject<TokenRequest>(environmentVariableValue);
            if (string.IsNullOrEmpty(userData.UserName) || string.IsNullOrEmpty(userData.Password))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Mensaje = "Información incompleta de usuario/contraseña para generar Token.";
                return response;
            }

            var parameters = new { UserName = userData.UserName, Password = userData.Password };
            var jsonData = JsonConvert.SerializeObject(parameters);
            string baseAddress = ConfigurationManager.AppSettings["urlBaseApi"];
            string endPoint = ConfigurationManager.AppSettings["urlToken"];

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(endPoint, content);
                    response = await result.Content.ReadAsAsync<ApiResponse>();
                    //if (result.IsSuccessStatusCode)
                    //{
                    //    ApiResponse tokenResponse = await result.Content.ReadAsAsync<ApiResponse>();
                    //    if (tokenResponse != null && tokenResponse.StatusCode == HttpStatusCode.OK)
                    //    {
                    //        var data = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Resultado.ToString());
                    //        accessToken = data.AccessToken;
                    //        message = "(GetToken) Extracción del token correcta.";
                    //    }
                    //}
                }
                Console.WriteLine(response.Mensaje);
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = "(GetToken) Error al generar TOKEN. " + GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
                Console.WriteLine(response.Mensaje);
            }

            return response;
        }

        private static async Task<ApiResponse> ExecuteLoadingProcess(string accessToken)
        {
            ApiResponse response = new ApiResponse { IsExitoso = false };

            string message = string.Empty;
            string baseAddress = ConfigurationManager.AppSettings["urlBaseApi"];
            string endPoint = ConfigurationManager.AppSettings["urlExcelLoad"];

            //Console.WriteLine("accessToken => " + accessToken);
            //Console.WriteLine("baseAddress => " + baseAddress);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    //var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                    //content.Headers.ContentType.CharSet = string.Empty;
                    //var response = await client.PostAsync(endPoint, content);

                    var result = await client.PostAsync(endPoint, null);
                    response = await result.Content.ReadAsAsync<ApiResponse>();
                    //if (result.IsSuccessStatusCode)
                    //{
                    //    var apiResponse = await result.Content.ReadAsAsync<ApiResponse>();
                    //    if (apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK)
                    //    {
                    //        message = apiResponse.Mensaje;
                    //    }
                    //    else
                    //    {
                    //        message = "(ExecuteLoadingProcess-OK) " + apiResponse;
                    //    }
                    //}
                    //else
                    //{
                    //    var result = await response.Content.ReadAsStringAsync();
                    //    var data = JsonConvert.DeserializeObject<ApiResponse>(result);
                    //    message = $"(ExecuteLoadingProcess) Code: {data.StatusCode} => Message: {data.Mensaje}";
                    //}
                }
                Console.WriteLine(response.Mensaje);
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = "(ExecuteLoadingProcess) Error al ejecutar la carga. " + GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
                Console.WriteLine(response.Mensaje);
            }

            return response;
        }

        private static string GetExceptionMessage(Exception exception)
        {
            string message = exception.Message + " ";
            while (exception?.InnerException != null)
            {
                message += exception.InnerException?.Message;
                exception = exception.InnerException;
            }

            return message;
        }

        #endregion

    }
}
