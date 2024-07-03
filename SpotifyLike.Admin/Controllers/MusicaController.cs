using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Streaming;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Admin.Controllers
{
    public class MusicaController : Controller
    {
        private MusicaService musicaService;
        private AlbumService albumService;

        public MusicaController(MusicaService musicaService, AlbumService albumService) 
        {
            this.musicaService = musicaService;
            this.albumService = albumService;
        }
        public IActionResult Index()
        {
            var result = this.musicaService.Obter();
            return View(result);
        }

        public IActionResult Criar()
        {
            var albuns = this.albumService.Obter();
            var result = new MusicaViewModel
            {
                Albuns = albuns
            };
            return View(result);
        }
        public IActionResult Salvar(MusicaViewModel musicaViewModel)
        {
            ModelState.Remove("Albuns");
            if (ModelState.IsValid == false)
            {
                musicaViewModel.Albuns = this.albumService.Obter();
                return View("Criar", musicaViewModel);
            }

            this.albumService.AssociarMusica(musicaViewModel.MusicaDto, musicaViewModel.AlbumId);
            
            return RedirectToAction("Index");
        }
    }
}
