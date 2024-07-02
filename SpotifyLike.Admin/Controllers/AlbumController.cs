using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming;

namespace SpotifyLike.Admin.Controllers
{
    public class AlbumController : Controller
    {
        private AlbumService albumService;
        private BandaService bandaService;

        public AlbumController(AlbumService albumService, BandaService bandaService)
        {
            this.albumService = albumService;
            this.bandaService = bandaService;
        }
        public IActionResult Index()
        {
            var result = this.albumService.Obter();
            return View(result);
        }

        public IActionResult Criar()
        {
            var bandas = this.bandaService.Obter();
            var result = new AlbumViewModel
            {
                Bandas = bandas
            };
            return View(result);
        }
    }
}
