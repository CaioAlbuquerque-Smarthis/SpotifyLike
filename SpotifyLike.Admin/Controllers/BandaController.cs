using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Admin.Dto;
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

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(BandaDto dto)
        {
            if (ModelState.IsValid == false)
                return View("Criar");

            this.bandaService.Criar(dto);

            return RedirectToAction("Index");
        }
    }
}
