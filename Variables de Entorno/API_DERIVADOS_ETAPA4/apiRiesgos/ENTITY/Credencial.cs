namespace ENTITY
{
    public class Credencial
    {
        public string grant_type { get; set; } = string.Empty;
        public string client_id { get; set; } = string.Empty;
        public string client_secret { get; set; } = string.Empty;
        public string scope { get; set; } = string.Empty;
    }
}
