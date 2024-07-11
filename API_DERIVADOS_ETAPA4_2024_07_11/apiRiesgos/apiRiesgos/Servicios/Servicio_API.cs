using ENTITY;
using Microsoft.VisualStudio.Services.OAuth;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace apiRiesgos.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private static string? _grant_type;
        private static string? _scope;
        private static string? _urlCom;
        private static string? _urlTok;
        private static string? _urlRiesgos;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _grant_type = builder.GetSection("ApiSettings:grant_type").Value;
            _scope = builder.GetSection("ApiSettings:scope").Value;
            _urlCom = builder.GetSection("ApiSettings:urlCom").Value;
            _urlTok = builder.GetSection("ApiSettings:urlTok").Value;
            _urlRiesgos = builder.GetSection("ApiSettings:urlRiesgos").Value;
        }

        public async Task<Respuesta> Conexion()
        {
            Respuesta resultado = new Respuesta();
            resultado.exito = false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_urlCom);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //GET Method
                    HttpResponseMessage response = await client.GetAsync("api/riesgos/ws");
                    if (response.IsSuccessStatusCode)
                    {
                        resultado.exito = true;
                    }
                    else
                    {
                        resultado.mensaje = "Internal server Error";
                    }
                }
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                resultado.mensaje = "Timed out: " + ex.Message;
            }
            catch (TaskCanceledException ex)
            {
                resultado.mensaje = "Canceled: " + ex.Message;
            }
            catch (Exception ex)
            {
                resultado.mensaje = ex.Message;
            }

            return resultado;
        }

        public async Task<ResultadoCredencial> Autenticacion(string client_id, string client_secret)
        {
            ResultadoCredencial resultado = new ResultadoCredencial();
            resultado.exito = false;
            AccessTokenResponse token = null;

            try
            {
                HttpClient client = HeadersForAccessTokenGenerate();
                string body = "grant_type=" + _grant_type.ToString() + "&client_id=" + client_id + "&client_secret=" + client_secret + "&scope=" + _scope.ToString();
                client.BaseAddress = new Uri(_urlTok);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Content = new StringContent(body,
                                                    Encoding.UTF8,
                                                    "application/x-www-form-urlencoded");//CONTENT-TYPE header

                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

                postData.Add(new KeyValuePair<string, string>("grant_type", _grant_type));
                postData.Add(new KeyValuePair<string, string>("client_id", client_id));
                postData.Add(new KeyValuePair<string, string>("client_secret", client_secret));
                postData.Add(new KeyValuePair<string, string>("scope", _scope));

                request.Content = new FormUrlEncodedContent(postData);
                HttpResponseMessage tokenResponse = client.PostAsync("/auth/access-token", new FormUrlEncodedContent(postData)).Result;

                token = await tokenResponse.Content.ReadAsAsync<AccessTokenResponse>(new[] { new JsonMediaTypeFormatter() });

                if (token.AccessToken != null)
                {
                    resultado.token = token.AccessToken;
                    resultado.exito = true;
                }
                else
                {
                    resultado.mensaje = "El Id o el password son incorrectos.";
                }
            }
            catch (HttpRequestException ex)
            {
                resultado.mensaje = "Error: " + ex.Message;
            }

            return resultado;
        }

        private HttpClient HeadersForAccessTokenGenerate()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);

            client.BaseAddress = new Uri(_urlTok);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            return client;
        }


        public async Task<ResultadoAPI> GetRiesgos(int reporte, int fecha, string token)
        {
            string serviceUrl = _urlRiesgos + "/api/riesgos";
            ResultadoAPI result = new ResultadoAPI();
            result.mensaje = "Error";
            try
            {
                HttpClient client = this.Method_Headers(token, serviceUrl);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                //client.Timeout = TimeSpan.FromMinutes(5);
                Uri myUri = new Uri(client.BaseAddress?.ToString() + "/" + reporte.ToString() + "/" + fecha.ToString());
                //HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress?.ToString() + "/" + reporte.ToString() + "/" + fecha.ToString()));
                HttpResponseMessage tokenResponse = await client.GetAsync(myUri.AbsoluteUri);
                if (tokenResponse.IsSuccessStatusCode)
                {
                    //result.mensaje = "Operación exitosa.";
                    result.mensaje = string.Empty;
                    switch (reporte)
                    {
                        case 1:
                            result.ListaValuacionReportos = tokenResponse.Content.ReadAsAsync<List<ValuacionReportos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 2:
                            result.ListaTenenciaTitulos = tokenResponse.Content.ReadAsAsync<List<TenenciaTitulos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 3:
                            result.ListaComprasMesaDinero = tokenResponse.Content.ReadAsAsync<List<ComprasMesaDinero>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 4:
                            result.ListaComprasTesoreria = tokenResponse.Content.ReadAsAsync<List<ComprasTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 5:
                            result.ListaPosicionPatrimonial = tokenResponse.Content.ReadAsAsync<List<PosicionPatrimonial>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 6:
                            break;
                        case 7:
                            result.ListaReporteREVAME = tokenResponse.Content.ReadAsAsync<List<ReporteREVAME>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 8:
                            result.ListaPosicionCalculoVAR = tokenResponse.Content.ReadAsAsync<List<PosicionCalculoVAR>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 9:
                            result.ListaPosicionRegulatorios = tokenResponse.Content.ReadAsAsync<List<PosicionRegulatorios>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 10:
                            result.ListaReportePosicionTesoreria = tokenResponse.Content.ReadAsAsync<List<ReportePosicionTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 11:
                            result.ListaPosicionGlobalTitulos = tokenResponse.Content.ReadAsAsync<List<PosicionGlobalTitulos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 12:
                            result.ListaMovimientosTesoreria = tokenResponse.Content.ReadAsAsync<List<MovimientosTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 13:
                            result.ListaPosicionForwards = tokenResponse.Content.ReadAsAsync<List<PosicionForwards>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 14:
                            result.ListaFlujosSwaps = tokenResponse.Content.ReadAsAsync<List<FlujosSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 15:
                            result.ListaFlujosPosicionesPrimarias = tokenResponse.Content.ReadAsAsync<List<FlujosPosicionesPrimarias>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 16:
                            result.ListaCaracteristicasSwaps = tokenResponse.Content.ReadAsAsync<List<CaracteristicasSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 17:
                            result.ListaLlamadaMargen = tokenResponse.Content.ReadAsAsync<List<LlamadaMargen>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 18:
                            result.ListaPosicionPrimariaSwaps = tokenResponse.Content.ReadAsAsync<List<PosicionPrimariaSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 19:
                            result.ListaOperacionCVDivisas = tokenResponse.Content.ReadAsAsync<List<ReporteOperacionCVDivisas>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                        case 20:
                            result.ListaPosicionesPrimForwards = tokenResponse.Content.ReadAsAsync<List<PosicionesPrimForwards>>(new[] { new JsonMediaTypeFormatter() }).Result;
                            break;
                    }
                }
                else {
                    result.mensaje = tokenResponse.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                result.mensaje = this.GetExceptionMessage(ex);
            }

            return result;
        }

        private HttpClient Method_Headers(string accessToken, string endpointURL)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            //Instruccion que nos ayuda a omitir el certificado seguro
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            //handler.SslProtocols = System.Security.Authentication.SslProtocols.Ssl2 | System.Security.Authentication.SslProtocols.Ssl3;
            //handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => cert!.Verify();
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
            //{
            //    // local dev, just approve all certs
            //    if (development) return true;
            //    return errors == SslPolicyErrors.None;
            //};
            HttpClient client = new HttpClient(handler);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            //System.Net.ServicePointManager.Expect100Continue = false;

            client.BaseAddress = new Uri(endpointURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            return client;
        }

        public async Task<ResultadoAPI> GetRiesgosRango(int reporte, int fechaIni, int fechaFin, string token)
        {
            string serviceUrl = _urlRiesgos + "/api/riesgos";
            ResultadoAPI resp = new ResultadoAPI();
            try
            {
                string getSecurityQuestionEndPoint = serviceUrl;

                HttpClient client = this.Method_Headers(token, getSecurityQuestionEndPoint);
                //client.BaseAddress = new Uri(searchUserEndPoint);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString() + "/" + reporte.ToString() + "/" + fechaIni.ToString() + "/" + fechaFin.ToString()));

                if (reporte == 6)
                {
                    resp.ListaComprasVentasOperador = tokenResponse.Content.ReadAsAsync<List<ComprasVentasOperador>>(new[] { new JsonMediaTypeFormatter() }).Result;
                }
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message;
            }
            return resp;
        }

        public string GetExceptionMessage(Exception exception)
        {
            string message = exception.Message + " ";
            while (exception?.InnerException != null)
            {
                message += exception.InnerException?.Message;
                exception = exception.InnerException!;
            }

            return message;
        }

    }
}
