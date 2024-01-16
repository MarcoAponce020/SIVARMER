using AutoMapper;
using InterfazRiesgosSimefin_WEB.Models;
using InterfazRiesgosSimefin_WEB.Models.Dto;
using InterfazRiesgosSimefin_WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InterfazRiesgosSimefin_WEB.Controllers
{
    public class PortafoliosController : Controller
    {

        private readonly IPortafolioService _portafolioService;
        private readonly IMapper _mapper;
        public PortafoliosController(IPortafolioService portafolioService, IMapper mapper)
        {
            _portafolioService = portafolioService;
            _mapper = mapper;   
        }
        public async Task<IActionResult> IndexPortafolio()
        {
            List<PortafolioDto> portafolioList = new ();
            var response = await _portafolioService.ObtenerTodos<APIResponse>();
            if (response != null  && response.IsExitoso) {
                portafolioList = JsonConvert.DeserializeObject<List<PortafolioDto>>(Convert.ToString(response.Resultado));
            }


            return View(portafolioList);
            
        }
    }
}
