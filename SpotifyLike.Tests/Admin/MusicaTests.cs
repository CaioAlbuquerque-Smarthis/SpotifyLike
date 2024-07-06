using System;
using SpotifyLike.Admin.Controllers;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.ValueObject;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SpotifyLike.Repository.Repository;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository;
using Microsoft.EntityFrameworkCore;
using SpotifyLike.Admin.Models;
using Spotify.Application.Streaming.Profile;
using SpotifyLike.Application.Streaming.Profile;

namespace SpotifyLike.Tests.Admin
{
    public class MusicaTests
    {
        
        private readonly MusicaService _musicaService;
        private readonly MusicaController _controller;
        private readonly AlbumService _albumService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMapper _mapper;
        private readonly MusicaViewModel _musicaViewModel;
        private readonly MusicaViewModel _musicaViewModel2;

        public MusicaTests()
        {
            // Create a DbContextOptions with an in-memory database
            var options = new DbContextOptionsBuilder<SpotifyLikeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            /*var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MusicaProfile>();
                cfg.AddProfile<AlbumProfile>();
            });

            _mapper = config.CreateMapper();*/

            // Instantiate the SpotifyLikeContext with the in-memory options
            var context = new SpotifyLikeContext(options);

            var musicas = new List<Musica>
            {
                new Musica { Id = new Guid() , Nome = "Song1", Duracao = new Duracao(200) },
                new Musica { Id = new Guid(), Nome = "Song2", Duracao = new Duracao(300) }
            };
            context.Musicas.AddRange(musicas);
            context.SaveChanges();

            // Instantiate the repository with the instantiated context
            var musicaRepository = new MusicaRepository(context);

            var albuns = new List<Album>
            {
                new Album { Id = new Guid(), Nome = "Album1", Musica = new List<Musica>() },
                new Album { Id = new Guid(), Nome = "Album2", Musica = new List<Musica>() }
            };
            context.Albuns.AddRange(albuns);
            context.SaveChanges();

            var albumRepository = new AlbumRepository(context);

            _mapperMock = new Mock<IMapper>();
            var musicaDtos = new List<MusicaDto>
            {
                new MusicaDto { Nome = "Song1", Duracao = new Duracao(200) },
                new MusicaDto { Nome = "Song2", Duracao = new Duracao(300) }
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<MusicaDto>>(musicas)).Returns(musicaDtos);
            _mapperMock.Setup(m => m.Map<Musica>(It.Is<MusicaDto>(dto => dto.Nome == "Song1" && dto.Duracao.Valor == 200)))
                .Returns(musicas.First);

            var albunsDtos = new List<AlbumDto>
            {
                new AlbumDto { Nome = "Album1", Musicas = new List<MusicaDto>() },
                new AlbumDto { Nome = "Album2", Musicas = new List<MusicaDto>() }
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<AlbumDto>>(It.IsAny<IEnumerable<Album>>())).Returns(albunsDtos);

            var album = new Album
            {
                Id = albuns.First().Id,
                Nome = "Album1",
                Musica = new List<Musica>
                 {
                    new Musica {Id = musicas.First().Id, Nome = "Song1", Duracao = new Duracao(200) }
                 }
            };

            var albumDto = new AlbumDto
            {
                Id = albuns.First().Id,
                Nome = "Album1",
                Musicas = new List<MusicaDto>
                {
                    new MusicaDto {Id = musicas.First().Id, Nome = "Song1", Duracao = new Duracao(200) }
                }
            };

            _mapperMock.Setup(m => m.Map<AlbumDto>(It.Is<Album>(a => a.Id == album.Id && a.Nome == album.Nome && a.Musica == album.Musica)))
           .Returns(albumDto);

            _musicaViewModel = new MusicaViewModel
            {
                Albuns = albunsDtos,
                AlbumId = albuns.First().Id,
                MusicaDto = musicaDtos.First()
            };
            _musicaViewModel2 = new MusicaViewModel
            {
                Albuns = albunsDtos,
                AlbumId = albuns.First().Id,
                MusicaDto = musicaDtos.ElementAt(1)
            };

            _musicaService = new MusicaService(musicaRepository, _mapperMock.Object);
            //_musicaService = new MusicaService(musicaRepository, _mapper);

            _albumService = new AlbumService(albumRepository, _mapperMock.Object);
            //_albumService = new AlbumService(albumRepository, _mapper);

            _controller = new MusicaController(_musicaService, _albumService);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfMusicaDtos()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MusicaDto>>(viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        [Fact]

        public void Criar_ReturnsViewResult_WithAllAlbumsInRepository()
        {
            var result = _controller.Criar();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicaViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Albuns.Count());
        }

        [Fact]

        public void Salvar_ReturnsViewResult_WithAllAlbumsInRepository()
        {
            var result = _controller.Salvar(_musicaViewModel);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
