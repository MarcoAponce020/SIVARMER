using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSerAPIRiesgos.Models
{

    /// <summary>
    /// Variables de entorno declaradas a nivel Sistema Operativo
    /// </summary>
    public class EnvironmentVariables
    {

        public DataBaseVariables DataBase { get; set; }

        public EmailVariables Email { get; set; }

    }

    public class DataBaseVariables
    {

        public string ServerNameOrIP { get; set; } = string.Empty;

        public int Port { get; set; } = 0;

        public string Scheme { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }

    public class EmailVariables
    {

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string EncryptionKey { get; set; } = string.Empty;

    }

}
