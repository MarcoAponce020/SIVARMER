using AutoMapper;
using InterfazRiesgosSimefin_API.Models;
using InterfazRiesgosSimefin_API.Models.Dto;

namespace InterfazRiesgosSimefin_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Portafolio,PortafolioDto>();
            CreateMap<PortafolioDto, Portafolio>();

            CreateMap<Portafolio, PortafolioCreateDto>().ReverseMap();
            CreateMap<Portafolio, PortafolioUpdateDto>().ReverseMap();

            //CreateMap<PortafolioDto, Portafolio>()
            //    .ForMember(x => x.ListaFechas, x => x.MapFrom(y => y.ListaFechas))
            //    .ReverseMap();

        }
    }
}
