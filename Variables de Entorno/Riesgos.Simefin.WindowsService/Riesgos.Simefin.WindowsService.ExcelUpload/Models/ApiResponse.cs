using System.Collections.Generic;
using System.Net;

namespace Riesgos.Simefin.WindowsService.ExcelLoad.Models
{

    /// <summary>
    /// Estructura de respuesta de Api invocada
    /// </summary>
    public class ApiResponse
    {

        public HttpStatusCode StatusCode { get; set; }

        public bool IsExitoso { get; set; } = false;

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public object Resultado { get; set; }

        public string Mensaje { get; set; }

    }

}
