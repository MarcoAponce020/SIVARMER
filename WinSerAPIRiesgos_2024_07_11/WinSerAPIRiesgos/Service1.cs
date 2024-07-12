using log4net.Config;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using WinSerAPIRiesgos.Helpers;
using WinSerAPIRiesgos.Models;

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

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            log.Info("===================================================================");
            log.Info("Se ejecuto el proceso del API SIVARMER : " + DateTime.Now.ToString());

            //string hour = "11:57 a. m.";
            string hour = "10:41 a. m.";

            if (DateTime.Now.ToShortTimeString() == hour)
            {
                string rToken = this.GetToken().Result;
                string correoBody = "";
                int diasDeCarga = 0;//dias que se cargará la información

                if (DateTime.Now.DayOfWeek != DayOfWeek.Monday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasDeCarga = -1;
                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    diasDeCarga = -3;
                }

                var fechaReporte = DateTime.Now.AddDays(diasDeCarga).ToString("yyyyMMdd");

                correoBody += ejecutarReporte(1, "Valuación de Reportos", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(2, "Tenencia de Títulos", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(3, "Compras Mesa de Dinero", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(4, "Compras Tesorería", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(5, "Posición Patrimonial", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(7, "Reporte REVAME", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(8, "Posición para Cálculo VAR", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(9, "Posición para Regulatorios", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(10, "Posición de Tesorería", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(11, "Posición Global Títulos", fechaReporte, rToken).Result + "<br>";
                correoBody += ejecutarReporte(12, "Movimientos de Tesorería", fechaReporte, rToken).Result + "<br>";

                EnviarEvidencia(correoBody, DateTime.Now.ToString());
                log.Info("Se ejecuto el proceso del API SIVARMER : " + DateTime.Now.ToString());
            }
        }

        /// <summary>
        /// Obtener Token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken()
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

        public async void EnviarEvidencia(string comentarios, string fecOper)
        {
            Respuesta response = new Respuesta { exito = false };

            string user = string.Empty; //usuario del correo.
            string password = string.Empty; //password del usuario
            string environmentVariable = ConfigurationManager.AppSettings.Get("EnvironmentVariable");
            string environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariable);
            if (!string.IsNullOrEmpty(environmentVariableValue))
            {
                var data = JsonConvert.DeserializeObject<EnvironmentVariables>(environmentVariableValue);
                user = data.Email.UserName;
                password = data.Email.Password;
            }
            else 
            {
                response.mensaje = "Usuario | Contraseña incorrectos.";
                log.Error("Usuario | Contraseña incorrectos.");
                return;
            }

            try
            {
                string correoFrom = ConfigurationManager.AppSettings["CorreoSalida"]; //Correo de salida.
                string correoAsunto = ConfigurationManager.AppSettings["CorreoAsunto"];//Asunto del correo.

                MailMessage message = new MailMessage();
                SmtpClient smtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTP.Server.Name"], 25);
                smtpServer.EnableSsl = true;
                message.From = new MailAddress(correoFrom, correoAsunto, System.Text.Encoding.UTF8);
                message.Subject = correoAsunto;

                var mailList = await this.GetMailsFromDataBase();
                if (mailList.Any() && mailList.Count > 0) {
                    foreach (var item in mailList)
                    {
                        message.To.Add(item.EmailAddress);
                        log.Info("Correo destino: " + item.EmailAddress);
                    }

                    message.IsBodyHtml = true;
                    message.Body = this.GetMailTemplate(comentarios);

                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP.Server.Name"], 25);//172.17.206.200
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(user, password, "banobras");
                    client.EnableSsl = true;
                    client.Send(message);

                    response.exito = true;
                    response.mensaje = "El correo fue enviado exitosamente.";
                    log.Info("El correo fue enviado exitosamente.");
                }
                else
                {
                    response.exito = true;
                    response.mensaje = "No se encontraron direcciones de correo para enviar.";
                    log.Warn("No se encontraron direcciones de correo para enviar.");
                }
            }
            catch (Exception ex)
            {
                response.mensaje = "Hubo un error al enviar el correo.";
                log.Error("Hubo un error al enviar el correo." + ex.Message);
            }
        }

        /// <summary>
        /// Plantilla del correo
        /// </summary>
        /// <param name="comentarios"></param>
        /// <returns></returns>
        private string GetMailTemplate(string comentarios)
        { 
            string tempalte = "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
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

            return tempalte;
        }

        /// <summary>
        /// Obtener direcciones de correo
        /// </summary>
        /// <returns></returns>
        private async Task<List<Mail>> GetMailsFromDataBase()
        {
            List<Mail> mailList = new List<Mail>();
            var connection = OracleConnectionHelper.GetConnection();
            string strSQL = "SELECT * FROM VAR_TD_USUARIOS_NOTIFICACIONES WHERE IsActive = 1";

            using (OracleCommand cmd = new OracleCommand(strSQL, connection))
            {
                await connection.OpenAsync();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                cmd.BindByName = true;

                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    var result = reader.Cast<IDataRecord>()
                                        .Select(dr => new Mail
                                        {
                                            EmailAddress = dr["EmailAddress"].ToString(),
                                            UserName = dr["UserName"].ToString(),
                                            IsActive = true
                                        });

                    mailList = result.ToList();

                    //while (reader.Read())
                    //{
                    //    mailList.Add(new Mail
                    //    {
                    //        EmailAddress = reader["EmailAddress"].ToString(),
                    //        UserName = reader["UserName"].ToString(),
                    //        //IsActive = (bool)reader["IsActive"]
                    //    });
                    //}
                }
                reader.Close();
                reader.Dispose();
            }

            return mailList;
        }

    }
}
