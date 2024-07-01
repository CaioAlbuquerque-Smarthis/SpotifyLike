using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Admin.Controllers
{
    public class MusicaController : Controller
    {
        private MusicaService musicaService;
        private BandaService bandaService;

        public MusicaController(MusicaService musicaService, BandaService bandaService) 
        {
            this.musicaService = musicaService;
            this.bandaService = bandaService;
        }
        public IActionResult Index()
        {
            var result = this.musicaService.Obter();
            return View(result);
        }

        public IActionResult Criar()
        {
            var bandas = this.bandaService.Obter();
            var result = new MusicaViewModel
            {
                Bandas = bandas
            };
            return View(result);
        }
    }
}
