using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Admin.Controllers
{
    public class MusicaController : Controller
    {
        private MusicaService musicaService;

        public MusicaController(MusicaService musicaService) 
        {
            this.musicaService = musicaService;
        }
        public IActionResult Index()
        {
            var result = this.musicaService.Obter();
            return View(result);
        }
    }
}
