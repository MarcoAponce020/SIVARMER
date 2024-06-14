using ENTITY;

namespace apiRiesgos.Servicios
{
    public interface IServicio_API
    {
        Task<Respuesta> Conexion();

        Task<ResultadoCredencial> Autenticacion(string client_id, string client_secret);

        Task<ResultadoAPI> GetRiesgos(int tipoReporte, int fechaConsulta, string token);

        Task<ResultadoAPI> GetRiesgosRango(int tipoReporte, int fechaIni, int fechaFin, string token);

    }
}
