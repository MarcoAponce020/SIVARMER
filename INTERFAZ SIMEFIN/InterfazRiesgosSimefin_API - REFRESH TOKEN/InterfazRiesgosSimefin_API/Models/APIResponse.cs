﻿using System.Net;

namespace InterfazRiesgosSimefin_API.Models
{
    public class APIResponse
    {
        public APIResponse()
        { 
            ErrorMessages= new List<string>();
        }
        public HttpStatusCode statusCode {  get; set; }
        public bool IsExitoso { get; set; } = true;
        public List<string>ErrorMessages { get; set; }
        public object Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}
