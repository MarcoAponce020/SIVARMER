namespace Riesgos.Simefin.Domain.Entities
{

    /// <summary>
    /// Variables de entorno declaradas a nivel Sistema Operativo
    /// </summary>
    public class EnvironmentVariables
    {

        public string ServerNameOrIP { get; set; } = string.Empty;

        public int Port { get; set; } = 0;

        public string Scheme { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

    }

}
