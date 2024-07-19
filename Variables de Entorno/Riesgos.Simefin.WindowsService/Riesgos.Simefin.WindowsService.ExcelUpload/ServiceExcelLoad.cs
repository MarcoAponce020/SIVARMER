using log4net;
using System.Configuration;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using Riesgos.Simefin.WindowsService.ExcelLoad.Models;
using log4net.Config;

namespace Riesgos.Simefin.WindowsService.ExcelLoad
{
    public partial class ServiceExcelLoad : ServiceBase
    {
        private static readonly ILog log = LogManager.GetLogger("WSPortfolioLoad");
        Timer _timer = new Timer();

        static ServiceExcelLoad()
        {
            DOMConfigurator.Configure();
        }

        public ServiceExcelLoad()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Info(Environment.NewLine);
            log.Info("===================================================================");
            log.Info("Servicio de proceso de carga iniciado : " + DateTime.Now.ToString());
            log.Info("===================================================================");

            // TODO: agregar código aquí para iniciar el servicio.
            string duracionIntervalo = ConfigurationManager.AppSettings["TimeIntervalPerMinute"];
            string minutosEnEjecucion = ConfigurationManager.AppSettings["MinutesRunning"];
            int intervalo = Convert.ToInt32(duracionIntervalo) * Convert.ToInt32(minutosEnEjecucion);

            Console.WriteLine("duracionIntervalo => " + duracionIntervalo);
            Console.WriteLine("minutosEnEjecucion => " + minutosEnEjecucion);
            Console.WriteLine("intervalo => " + intervalo);

            _timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            _timer.Interval = intervalo; //cada minuto, valor en milisegundos | 1 min => 60000
            _timer.Enabled = true;
            _timer.Start();
        }

        protected override void OnStop()
        {
            log.Info("===================================================================");
            log.Info("Servicio de proceso de carga detenido : " + DateTime.Now.ToString());
            log.Info("===================================================================");

            _timer.Stop();
            _timer.Dispose();
        }

        private async void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            string executeTime = ConfigurationManager.AppSettings["ExecuteTime"];
            if (DateTime.Now.ToShortTimeString() == executeTime)
            {

                Console.WriteLine("executeTime 1 => " + DateTime.Now.ToShortTimeString());
                Console.WriteLine("executeTime 2 => " + executeTime);

                //_timer.Enabled = false; //Nueva
                log.Info("===================================================================");
                log.Info("Inicio del proceso de carga : " + DateTime.Now.ToString());
                log.Info("Buscando Token");
                string token = await Task.Run(() => GetToken());
                log.Info($"Token obtenido => {token}");
                if (!string.IsNullOrEmpty(token))
                {
                    log.Info("Iniciar proceso de carga : " + DateTime.Now.ToString());
                    var result = this.ExecuteLoadingProcess(token);
                    log.Info("Se ejecuto el proceso de carga : " + DateTime.Now.ToString());
                }
            }
        }

        /// <summary>
        /// Ejecutar proceso de carga
        /// </summary>
        /// <param name="accessToken">Token de acceso</param>
        /// <returns></returns>
        private async Task<string> ExecuteLoadingProcess(string accessToken)
        {
            string message = string.Empty;
            string baseAddress = ConfigurationManager.AppSettings["urlBaseApi"];
            string endPoint = ConfigurationManager.AppSettings["urlExcelLoad"];

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var response = await client.PostAsync(endPoint, null);
                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsAsync<ApiResponse>();
                        if (apiResponse != null && apiResponse.StatusCode == HttpStatusCode.OK)
                        {
                            message = apiResponse.Mensaje;
                            log.Info(message);
                        }
                        else
                        {
                            log.Info("(ExecuteLoadingProcess-OK) " + apiResponse);
                        }
                    }
                    else
                    {
                        log.Error("(ExecuteLoadingProcess) " + response.StatusCode);
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            message = "(ExecuteLoadingProcess) " + response.ReasonPhrase;
                        }
                        log.Error(message);
                    }
                }
                //_timer.Enabled = true; //Nueva
            }
            catch (Exception ex)
            {
                message = "(ExecuteLoadingProcess) Error al ejecutar la carga. " + this.GetExceptionMessage(ex);
                log.Error(message);
            }

            return message;
        }

        /// <summary>
        /// Obtener Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken()
        {
            string message = string.Empty;
            string baseAddress = ConfigurationManager.AppSettings["urlBaseApi"];
            string endPoint = ConfigurationManager.AppSettings["urlToken"];
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
            string accessToken = "";

            var parameters = new { UserName = userName, Password = password };
            var jsonData = JsonConvert.SerializeObject(parameters);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(endPoint, content);
                    if (response.IsSuccessStatusCode)
                    {
                        ApiResponse tokenResponse = await response.Content.ReadAsAsync<ApiResponse>();
                        if (tokenResponse != null && tokenResponse.StatusCode == HttpStatusCode.OK)
                        {
                            var result = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Resultado.ToString());
                            accessToken = result.AccessToken;
                            message = "(GetToken) Extracción del token correcta.";
                            log.Info(message);
                        }
                        else
                        {
                            log.Info("(GetToken-OK) " + tokenResponse);
                        }
                    }
                    else
                    {
                        log.Error("(GetToken) " + response.StatusCode);
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            message = "(GetToken) " + response.ReasonPhrase;
                        }
                        log.Error(message);
                    }
                }
            }
            catch (Exception ex)
            {
                message = "(GetToken) Error al generar TOKEN. " + this.GetExceptionMessage(ex);
                log.Error(message);
            }

            return accessToken;
        }

        /// <summary>
        /// Recuperar mensajes de error anidados
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetExceptionMessage(Exception exception)
        {
            string message = exception.Message + " ";
            while (exception?.InnerException != null)
            {
                message += exception.InnerException?.Message + " ";
                exception = exception.InnerException;
            }

            return message;
        }


    }
}
