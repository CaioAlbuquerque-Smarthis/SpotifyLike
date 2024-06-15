using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;

namespace SpotifyLike.Admin.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public IActionResult Criar()
        {
            return View();
        }

        [AllowAnonymous]
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
