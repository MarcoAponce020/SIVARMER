using Microsoft.AspNetCore.Mvc;
using Riesgos.Simefin.Application.DTOs;
using System.Net;

namespace Riesgos.Simefin.WebAPI.Helpers
{

    public class StatusResponseHelper
    {

        /// <summary>
        /// Obteniendo el Status final a retornar
        /// </summary>
        /// <param name="response">Respuesta del servicio</param>
        /// <returns></returns>
        public static HttpResponseMessage GetStatusResponse(ResponseDTO response)
        {
            // IHttpActionResult 
            // HttpResponseMessage
            // ObjectResult
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return (HttpResponseMessage)Results.BadRequest(response);
                case HttpStatusCode.OK:
                    return (HttpResponseMessage)Results.Ok(response);
                case HttpStatusCode.NotFound:
                    return (HttpResponseMessage)Results.NotFound(response);
                default:
                    return (HttpResponseMessage)Results.Ok();
            }
        }

    }

}
