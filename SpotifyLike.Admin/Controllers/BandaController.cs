using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Controllers
{
    public class BandaController : Controller
    {
        private BandaService bandaService;

        public BandaController(BandaService bandaService) 
        {
            this.bandaService = bandaService;
        }
        public IActionResult Index()
        {
            var result = this.bandaService.Obter();
            return View(result);
        }
    }
}
