namespace Riesgos.Simefin.WindowsService.PortfolioLoad.Models
{

    /// <summary>
    /// Atributos para generar un Token
    /// </summary>
    public class TokenRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }

}
