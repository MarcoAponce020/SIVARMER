using ENTITY;

namespace apiRiesgos.Servicios
{
    public interface IServicioApi
    {
        Task<Respuesta> Conexion();

        Task<ResultadoCredencial> Autenticacion(string client_id, string client_secret);

        Task<ResultadoApi> GetRiesgos(int tipoReporte, int fechaConsulta, string token);

        Task<ResultadoApi> GetRiesgosRango(int tipoReporte, int fechaIni, int fechaFin, string token);

    }
}
