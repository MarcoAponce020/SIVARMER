using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Configuration;
using log4net.Config;

namespace WinSerAPIRiesgos
{
    public partial class Service1 : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("LogApiSivarmer");
        Timer _timer = new Timer(60000);

        static Service1()
        {
            DOMConfigurator.Configure();
        }
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            log.Info("Se ejecuto el proceso del API SIVARMER : " + DateTime.Now.ToString());
            string time = DateTime.Now.ToShortTimeString();

            if (DateTime.Now.ToShortTimeString() == "11:57 a. m.")
            {
                string rToken = getToken().Result;
                string correoBody = "";
                int d = 0;//dias que se cargará la información

                if (DateTime.Now.DayOfWeek != DayOfWeek.Monday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    d = -1;

                }
                else
                {
                    if(DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    {
                        d = -3;

                    }
                }

                correoBody += ejecutarReporte(1, "Valuación de Reportos", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(2, "Tenencia de Títulos", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(3, "Compras Mesa de Dinero", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(4, "Compras Tesorería", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(5, "Posición Patrimonial", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(7, "Reporte REVAME", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(8, "Posición para Cálculo VAR", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(9, "Posición para Regulatorios", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(10, "Posición de Tesorería", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(11, "Posición Global Títulos", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";
                correoBody += ejecutarReporte(12, "Movimientos de Tesorería", DateTime.Now.AddDays(d).ToString("yyyyMMdd"), rToken).Result + "<br>";

                EnviarEvidencia(correoBody, DateTime.Now.ToString());

                log.Info("Se ejecuto el proceso del API SIVARMER : " + DateTime.Now.ToString());
            }
        }
        protected override void OnStop()
        {
            Console.WriteLine("Stopping. Here's one method where you'd handle processes that shouldn't be interrupted, gracefully stop them, then end the service. Press  to finish stopping.");
            _timer.Stop();
            _timer.Dispose();
            //etc.
            Console.ReadLine();
        }
        public void Start(string[] args)
        {
            OnStart(args);
        }
        public async Task<string> getToken()
        {
            string token = "";
            string URL = ConfigurationManager.AppSettings["urlToken"];
            string urlParameters = ConfigurationManager.AppSettings["urlParameters"];


            string serviceUrl = URL + urlParameters;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.GetAsync(urlParameters);
                    if (response.IsSuccessStatusCode)
                    {
                        ResultadoToken department = await response.Content.ReadAsAsync<ResultadoToken>();
                        token = department.token;
                        log.Info("Extracción del token correcta.");
                    }
                    else
                    {
                        log.Error("Internal server Error");
                    }
                }
            }
            catch (Exception ex)
            {
                token = "Error al generar TOKEN";
                log.Error("Error al generar TOKEN" + ex.Message);
            }

            return token;
        }
        public async Task<string> ejecutarReporte(int tipo,string nombre, string fecha, string token)
        {
            string Url = ConfigurationManager.AppSettings["urlReporte"];
            string correoBody = "";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.GetAsync(tipo.ToString() + "/" + fecha + "/" + token);
                    if (response.IsSuccessStatusCode)
                    {
                        Respuesta department = await response.Content.ReadAsAsync<Respuesta>();
                        log.Info("Para el reporte " + tipo +": " + department.mensaje);
                        correoBody = "Para el reporte " + nombre + ": " + department.mensaje;
                    }
                    else
                    {
                        log.Error("Internal server Error");
                    }
                }
            }
            catch (Exception ex)
            {
                correoBody = "Error al extraer información del reporte: " + tipo + ". ";
                log.Error("Error al extraer información del reporte: " + tipo + ". " + ex.Message);
            }
            return correoBody;

        }
        public void EnviarEvidencia(string comentarios, string fecOper)
        {
            Respuesta resp = new Respuesta();
            resp.exito = false;
            string textBody;
            string StCorreoFrom, StCorreoAsunto, Usser, Password;
            StCorreoFrom = string.Empty;
            StCorreoAsunto = String.Empty;
            Usser = string.Empty;
            Password = string.Empty;

            try
            {
                StCorreoFrom = ConfigurationManager.AppSettings["CorreoSalida"]; //Correo de salida.
                StCorreoAsunto = ConfigurationManager.AppSettings["CorreoAsunto"];//Asunto del correo.
                Usser = ConfigurationManager.AppSettings["CorreoUsuario"];//usuario del correo.
                Password = ConfigurationManager.AppSettings["CorreoPass"];//password del usuario

                MailMessage message = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("correo.banobras.gob.mx", 25);
                smtpServer.EnableSsl = true;
                message.From = new MailAddress(StCorreoFrom, StCorreoAsunto, System.Text.Encoding.UTF8);
                message.Subject = StCorreoAsunto;

                string[] correos = ConfigurationManager.AppSettings["Correos"].Split(',');

                foreach (var item in correos)
                {
                    message.To.Add(item);
                }

                textBody = "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                           "<head>" +
                           "    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />" +
                           "</head>" +
                           "<body>" +
                           "    <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                           "        <tr>" +
                           "            <td align=\"center\" valign=\"top\" bgcolor=\"#ffe77b\" style=\"background-color:#ffffff;\">" +
                           "                <br>" +
                           "                <br>" +
                           "                <table width=\"700\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                           "                    <tr>" +
                           "                        <td height=\"70\" align=\"left\" valign=\"middle\"></td>" +
                           "                    </tr>" +
                           "                    <tr>" +
                           "                        <td align=\"left\" valign=\"top\" bgcolor=\"#564319\" style=\"background-color:#621132; font-family:Arial, Helvetica, sans-serif; padding:10px;\">" +
                           "                            <div style=\"font-size:36px; color:#ffffff; text-align:center;\">" +
                           "                                <b>BANOBRAS</b>" +
                           "                            </div>" +
                           "                            <div style=\"font-size:13px; color:#a29881; text-align:center;\">" +
                           "                                <b>Banco Nacional de Obras y Servicios Públicos S.N.C</b>" +
                           "                            </div>" +
                           "                        </td>" +
                           "                    </tr>" +
                           "                    <tr>" +
                           "                        <td align=\"left\" valign=\"top\" bgcolor=\"#ffffff\" style=\"background-color:#ffffff;\">" +
                           "                            <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                           "                                <tr>" +
                           "                                    <td align=\"center\" valign=\"middle\" style=\"padding: 10px; color:#564319; font-size:28px; font-family:Georgia, 'Times New Roman', Times, serif;\">" +
                           "                                        Evidencia de ejecución WEB API SIVARMER" +
                           "                                    </td>" +
                           "                                </tr>" +
                           "                            </table>" +
                           "                            <table width=\"95%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">" +
                           "                                <tr>" +
                           "                                    <td width=\"0%\" align=\"center\" valign=\"middle\" style=\"padding: 10px; \">" +
                           "                                    </td>" +
                           "                                    <td align=\"left\" valign=\"middle\" style=\"color:#525252; font-family:Arial, Helvetica, sans-serif; padding:10px;\">" +
                           "                                        <div style=\"font-size:16px; \">" +
                           "                                            Comentarios:" +
                           "                                        </div>" +
                           "                                        <br />" +
                           "                                        <div style=\"font-size:12px; \">" + comentarios +
                           "                                            <hr>" +
                           "                                        </div>" +
                           "                                    </td>" +
                           "                                </tr>" +
                           "                            </table>" +
                           "                            <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"margin-bottom:10px; \">" +
                           "                                <tr>" +
                           "                                    <td align=\"left\" valign=\"middle\" style=\"padding: 15px; background-color:#621132; font-family:Arial, Helvetica, sans-serif;\">" +
                           "                                       <div style=\"font-size:13px; color:#a29881; text-align:center;\">" +
                           "                                            <b>WEB API DE DESARROLLO SIVARMER </b>" +
                           "                                        </div>" +
                           "                                    </td>" +
                           "                                </tr>" +
                           "                            </table>" +
                           "                        </td>" +
                           "                    </tr>" +
                           "                </table>" +
                           "                <br>" +
                           "                <br>" +
                           "            </td>" +
                           "        </tr>" +
                           "    </table>" +
                           "</body>" +
                           "</html>";

                //........................................................................................................

                message.IsBodyHtml = true;
                //........................................................................................................
                message.Body = textBody;//Body
                SmtpClient client = new SmtpClient("correo.banobras.gob.mx", 25);//172.17.206.200
                client.UseDefaultCredentials = false;

                client.Credentials = new System.Net.NetworkCredential(Usser, Password, "banobras");
                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                    resp.exito = true;
                    resp.mensaje = "El correo fue enviado exitosamente.";
                    log.Info("El correo fue enviado exitosamente.");
                }
                catch (Exception ex)
                {
                    resp.exito = false;
                    resp.mensaje = "Hubo un error al enviar el correo.";
                    log.Error("Hubo un error al enviar el correo." + ex.Message);
                }
                //==================================================================================================================
            }
            catch (Exception ex)
            {
                resp.exito = false;
                log.Error("Hubo un error al enviar el correo." + ex.Message);
            };
        }
    }
}
