using AutoMapper;
using Newtonsoft.Json;
using Riesgos.Simefin.Application.DTOs.Portfolio;
using Riesgos.Simefin.Application.DTOs.User;
using Riesgos.Simefin.Domain.Entities;

namespace Riesgos.Simefin.Application.Mapping
{

    public class AutoMappingProfile : Profile
    {

        public AutoMappingProfile()
        {
            CreateMap<Portafolio, PortafolioDTO>().ForMember(x => x.ListaDatos, list => list.MapFrom(ListaDatosMAP));
            CreateMap<PortafolioDTO, Portafolio>();

            CreateMap<Portafolio, PortafolioCreateDTO>().ReverseMap();
            CreateMap<Portafolio, PortafolioUpdateDTO>().ReverseMap();
            CreateMap<Usuario, UserDTO>().ReverseMap();
        }

        public List<ListaDatosDTO> ListaDatosMAP(Portafolio portafolio, PortafolioDTO portafolioDto)
        {
            var source = JsonConvert.DeserializeObject<List<ListaDatosDTO>>(portafolio.ListaDatos!);

            return source!;
        }

    }

}
