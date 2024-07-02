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
        public IActionResult Salvar(MusicaViewModel musicaViewModel)
        {
            if (ModelState.IsValid == false)
                return View("Criar");


            var albumDto = this.bandaService.ObterAlbum(musicaViewModel.BandaId)
                                         .Find(x => x.Nome.Equals(musicaViewModel.NomeAlbum));

            if (albumDto  == null)
            {
                var createdAlbumDto = new AlbumDto
                {
                    BandaId = musicaViewModel.BandaId,
                    Nome = musicaViewModel.NomeAlbum,
                    Musicas = new List<MusicaDto> { musicaViewModel.MusicaDto }
                };
                bandaService.AssociarAlbum(createdAlbumDto);
            }
            else 
            {

            }
            return RedirectToAction("Index");
        }
    }
}
