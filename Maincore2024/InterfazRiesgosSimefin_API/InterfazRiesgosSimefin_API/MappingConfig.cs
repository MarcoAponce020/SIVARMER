using AutoMapper;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;
using Newtonsoft.Json;

namespace InterfazRiesgosSimefin_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Portafolio, PortafolioDto>().ForMember(x => x.listaDatos, list => list.MapFrom(ListaDatosMAP));
            CreateMap<PortafolioDto, Portafolio>();

            CreateMap<Portafolio, PortafolioCreateDto>().ReverseMap();
            CreateMap<Portafolio, PortafolioUpdateDto>().ReverseMap();
        }

        /// <summary>
        /// Mapeando la cadena del campo ListaDatos en un objeto JSON
        /// </summary>
        /// <param name="portafolio"></param>
        /// <param name="portafolioDto"></param>
        /// <returns></returns>
        public List<ListaDatosDto> ListaDatosMAP(Portafolio portafolio, PortafolioDto portafolioDto) 
        {
            var source = JsonConvert.DeserializeObject<List<ListaDatosDto>> (portafolio.listaDatos);

            return source;
        }

    }
}
