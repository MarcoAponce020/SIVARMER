using AutoMapper;
using InterfazRiesgosSimefin_WEB.Models.Dto;

namespace InterfazRiesgosSimefin_WEB
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<PortafolioDto, PortafolioCreateDto>().ReverseMap();
            CreateMap<PortafolioDto, PortafolioUpdateDto>().ReverseMap();


        }
    }
}
