using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;

namespace SpotifyLike.Admin.Controllers
{
    public class UserController : Controller
    {
        private UsuarioAdminService usuarioAdministradorService;

        public UserController(UsuarioAdminService usuarioAdminService) 
        {
            this.usuarioAdministradorService = usuarioAdminService;
        }
        public IActionResult Index()
        {
            var result = this.usuarioAdministradorService.ObterTodos();
            return View(result);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(UsuarioAdminDto dto)
        {
            if (ModelState.IsValid == false)
                return View("Criar");

            this.usuarioAdministradorService.Salvar(dto);

            return RedirectToAction("Index");
        }
    }
}
