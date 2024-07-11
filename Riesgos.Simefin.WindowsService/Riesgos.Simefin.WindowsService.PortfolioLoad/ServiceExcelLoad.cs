using log4net;
using log4net.Config;
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
using System.Timers;

namespace Riesgos.Simefin.WindowsService.PortfolioLoad
{
    public partial class ServiceExcelLoad : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger("WSPortfolioLoad");
        Timer _timer = new Timer();

        static ServiceExcelLoad()
        {
            DOMConfigurator.Configure();
        }

        public ServiceExcelLoad()
        {
            InitializeComponent();
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _log.Info("Buscando Token ...");
            var tokenResponse = await Task.Run(() => GetToken());
            if (tokenResponse.IsExitoso)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(tokenResponse.Resultado.ToString());
                var result = await this.ExecuteLoadingProcess(token.AccessToken);
                if (result.IsExitoso)
                {
                    _log.Info(JsonConvert.SerializeObject(result));
                }
                else
                {
                    _log.Error(JsonConvert.SerializeObject(result));
                }
            }
            else 
            {
                _log.Warn(JsonConvert.SerializeObject(tokenResponse));
            }
        }

        /// <summary>
        /// Se ejecuta al momento de iniciar el servicio y/o SO
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // Código para iniciar el servicio.
            string duracionIntervalo = ConfigurationManager.AppSettings["TimeIntervalPerMinute"];
            string minutosEnEjecucion = ConfigurationManager.AppSettings["MinutesRunning"];
            int intervalo = Convert.ToInt32(duracionIntervalo) * Convert.ToInt32(minutosEnEjecucion);

            _timer.Enabled = true;
            _timer.Interval = intervalo; //cada minuto, valor en milisegundos | 1 min => 60000
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        /// <summary>
        /// Detener el servicio
        /// </summary>
        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        /// <summary>
        /// Ejecutar proceso de carga
        /// </summary>
        /// <param name="accessToken">Token de acceso</param>
        /// <returns></returns>
        private async Task<ApiResponse> ExecuteLoadingProcess(string accessToken)
        {
            ApiResponse response = new ApiResponse { IsExitoso = false };
            string baseAddress = ConfigurationManager.AppSettings["urlBaseApi"];
            string endPoint = ConfigurationManager.AppSettings["urlExcelLoad"];

            _log.Info("Iniciar proceso de carga ...");

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseAddress);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var result = await client.PostAsync(endPoint, null);
                    response = await result.Content.ReadAsAsync<ApiResponse>();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _log.Info(response.Mensaje);
                    }
                    else
                    {
                        _log.Warn(response.Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = "(ExecuteLoadingProcess) Error al ejecutar la carga. " + this.GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
                _log.Error(response.Mensaje);
            }

            _log.Info("Se ejecutó el proceso de carga.");
            return response;
        }

        /// <summary>
        /// Obtener Token
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetToken()
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
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _log.Info(response.Mensaje);
                    }
                    else
                    {
                        _log.Warn(response.Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsExitoso = false;
                response.Mensaje = "Error al generar TOKEN. " + GetExceptionMessage(ex);
                response.StatusCode = HttpStatusCode.BadRequest;
                _log.Error(response.Mensaje);
            }

            return response;
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
