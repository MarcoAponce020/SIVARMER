using ENTITY;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Microsoft.VisualStudio.Services.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Policy;
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
            ResultadoAPI resp = new ResultadoAPI();
            try
            {
                string getSecurityQuestionEndPoint = serviceUrl;

                HttpClient client = Method_Headers(token, getSecurityQuestionEndPoint);
                client.Timeout = TimeSpan.FromMinutes(5);
                //client.BaseAddress = new Uri(searchUserEndPoint);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString() + "/" + reporte.ToString() + "/" + fecha.ToString()));
                if (tokenResponse.IsSuccessStatusCode)
                {
                    if (reporte == 1)
                    {
                        resp.listaValuacionReportos = tokenResponse.Content.ReadAsAsync<List<ValuacionReportos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 2)
                    {
                        resp.listaTenenciaTitulos = tokenResponse.Content.ReadAsAsync<List<TenenciaTitulos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 3)
                    {
                        resp.listaComprasMesaDinero = tokenResponse.Content.ReadAsAsync<List<ComprasMesaDinero>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 4)
                    {
                        resp.listaComprasTesoreria = tokenResponse.Content.ReadAsAsync<List<ComprasTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 5)
                    {
                        resp.listaPosicionPatrimonial = tokenResponse.Content.ReadAsAsync<List<PosicionPatrimonial>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 7)
                    {
                        resp.listaReporteREVAME = tokenResponse.Content.ReadAsAsync<List<ReporteREVAME>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 8)
                    {
                        resp.listaPosicionCalculoVAR = tokenResponse.Content.ReadAsAsync<List<PosicionCalculoVAR>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 9)
                    {
                        resp.listaPosicionRegulatorios = tokenResponse.Content.ReadAsAsync<List<PosicionRegulatorios>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 10)
                    {
                        resp.listaReportePosicionTesoreria = tokenResponse.Content.ReadAsAsync<List<ReportePosicionTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 11)
                    {
                        resp.listaPosicionGlobalTitulos = tokenResponse.Content.ReadAsAsync<List<PosicionGlobalTitulos>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 12)
                    {
                        resp.listaMovimientosTesoreria = tokenResponse.Content.ReadAsAsync<List<MovimientosTesoreria>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 13)
                    {
                        resp.listaPosicionForwards = tokenResponse.Content.ReadAsAsync<List<PosicionForwards>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 14)
                    {
                        resp.listaFlujosSwaps = tokenResponse.Content.ReadAsAsync<List<FlujosSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                        
                    }
                    else if (reporte == 15)
                    {
                        resp.listaFlujosPosicionesPrimarias = tokenResponse.Content.ReadAsAsync<List<FlujosPosicionesPrimarias>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 16)
                    {
                        resp.listaCaracteristicasSwaps =  tokenResponse.Content.ReadAsAsync<List<CaracteristicasSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 17)
                    {
                        resp.listaLlamadaMargen = tokenResponse.Content.ReadAsAsync<List<LlamadaMargen>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 18)
                    {
                        resp.listaPosicionPrimariaSwaps = tokenResponse.Content.ReadAsAsync<List<PosicionPrimariaSwaps>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                    else if (reporte == 19)
                    {
                        resp.listaOperacionCVDivisas = tokenResponse.Content.ReadAsAsync<List<ReporteOperacionCVDivisas>>(new[] { new JsonMediaTypeFormatter() }).Result;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message;
            }
            return resp;
        }
        private HttpClient Method_Headers(string accessToken, string endpointURL)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);

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

                HttpClient client = Method_Headers(token, getSecurityQuestionEndPoint);
                //client.BaseAddress = new Uri(searchUserEndPoint);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString() + "/" + reporte.ToString() + "/" + fechaIni.ToString() + "/" + fechaFin.ToString()));

                if (reporte == 6)
                {
                    resp.listaComprasVentasOperador = tokenResponse.Content.ReadAsAsync<List<ComprasVentasOperador>>(new[] { new JsonMediaTypeFormatter() }).Result;
                }
            }
            catch (Exception ex)
            {
                resp.mensaje = ex.Message;
            }
            return resp;
        }
    }
}
