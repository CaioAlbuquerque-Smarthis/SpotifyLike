using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Conta;

namespace SpotifyLike.Admin.Controllers
{
    public class RelatorioController : Controller
    {
        private UsuarioService usuarioService;

        public RelatorioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            var result = this.usuarioService.GerarRelatorioMusicasFavoritas();
            return View(result);
        }
    }
}
