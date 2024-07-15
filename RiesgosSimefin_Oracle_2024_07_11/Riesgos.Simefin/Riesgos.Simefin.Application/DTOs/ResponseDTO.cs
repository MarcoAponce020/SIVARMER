using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Riesgos.Simefin.Application.DTOs
{

    public class ResponseDTO
    {

        public HttpStatusCode StatusCode { get; set; }

        public bool IsExitoso { get; set; } = false;

        public List<string> ErrorMessages { get; set; } = new List<string>();

        public object? Resultado { get; set; }

        public string? Mensaje { get; set; }


    }

}
