namespace Riesgos.Simefin.WindowsService.ExcelLoad.Models
{

    /// <summary>
    /// Estructura de respuesta de Token gerado
    /// </summary>
    public class TokenResponse
    {
        public string UserName { get; set; }

        public string Nombre { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

    }
}
