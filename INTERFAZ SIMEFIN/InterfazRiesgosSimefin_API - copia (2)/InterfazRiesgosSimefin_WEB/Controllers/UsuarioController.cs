using InterfazRiesgosSimefin_WEB.Models.Dto;
using InterfazRiesgosSimefin_WEB.Models;
using InterfazRiesgosSimefin_WEB.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InterfazRiesgosSimefin_WEB.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginRequestDTO modelo)
        {
            return View();

        }

        public IActionResult Registrar()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroRequestDTO modelo)
        {
            var response = await _usuarioService.Registrar<APIResponse>(modelo);
            if(response !=null && response.IsExitoso)
            {
                return RedirectToAction("login");
            }
            return View();

        }

        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }


    }
}
