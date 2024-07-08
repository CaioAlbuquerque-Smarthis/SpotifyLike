using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Conta;

namespace SpotifyLike.Admin.Controllers
{
    public class RelatorioController : Controller
    {
        private UsuarioService usuarioService;
        private RelatorioBandaService relatorioBandaService;

        public RelatorioController(UsuarioService usuarioService, RelatorioBandaService relatorioBandaService)
        {
            this.usuarioService = usuarioService;
            this.relatorioBandaService = relatorioBandaService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RelatorioMusica()
        {
            var result = this.usuarioService.GerarRelatorioMusicasFavoritas();
            return View(result);
        }

        public IActionResult RelatorioBanda()
        {
            var result = this.relatorioBandaService.GerarRelatorioBandasFavoritas();
            return View(result);
        }
    }
}
