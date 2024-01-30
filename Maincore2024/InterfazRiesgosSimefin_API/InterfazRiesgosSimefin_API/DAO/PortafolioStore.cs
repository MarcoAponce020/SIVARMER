using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using System.Threading;

namespace InterfazRiesgosSimefin_API.DAO
{
    public static class PortafolioStore
    {
        public static List<PortafolioDto> portafolioList = new List<PortafolioDto> {

            new PortafolioDto{IdPortafolio=1,F_Posicion="20231101",NombrePortafolio="TOTAL",SubPortafolio="P&L1", No_Envio= 6253, listaDatos="1", ListaFechas ="20240125" },
            new PortafolioDto{IdPortafolio=4, F_Posicion="20231101", NombrePortafolio="TOTAL",SubPortafolio="P&L2", No_Envio= -4311,listaDatos="2", ListaFechas ="20240125" },
            new PortafolioDto{IdPortafolio=3, F_Posicion="20231101", NombrePortafolio="TOTAL",SubPortafolio="P&L3", No_Envio= -4311,listaDatos="3" , ListaFechas = "20240125"}

         };
    }
}
