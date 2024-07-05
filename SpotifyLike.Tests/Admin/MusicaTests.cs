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

namespace SpotifyLike.Tests.Admin
{
    public class MusicaTests
    {
        
        private readonly MusicaService _musicaService;
        private readonly MusicaController _controller;
        private readonly AlbumService _albumService;
        private readonly Mock<IMapper> _mapperMock;

        public MusicaTests()
        {
            // Create a DbContextOptions with an in-memory database
            var options = new DbContextOptionsBuilder<SpotifyLikeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Instantiate the SpotifyLikeContext with the in-memory options
            var context = new SpotifyLikeContext(options);

            var musicas = new List<Musica>
            {
                new Musica { Nome = "Song1", Duracao = new Duracao(200) },
                new Musica { Nome = "Song2", Duracao = new Duracao(300) }
            };
            context.Musicas.AddRange(musicas);
            context.SaveChanges();

            // Instantiate the repository with the instantiated context
            var musicaRepository = new MusicaRepository(context);

            var albuns = new List<Album>
            {
                new Album { Nome = "Album1", Musica = new List<Musica>() },
                new Album { Nome = "Album2", Musica = new List<Musica>() }
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

            var albunsDtos = new List<AlbumDto>
            {
                new AlbumDto { Nome = "Album1", Musicas = new List<MusicaDto>() },
                new AlbumDto { Nome = "Album2", Musicas = new List<MusicaDto>() }
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<AlbumDto>>(It.IsAny<IEnumerable<Album>>())).Returns(albunsDtos);

            // Create a real instance of MusicaService with the mocked dependencies
            _musicaService = new MusicaService(musicaRepository, _mapperMock.Object);

            // Mock AlbumService (if needed)
            _albumService = new AlbumService(albumRepository, _mapperMock.Object); // Adjust parameters based on your constructor

            // Instantiate the controller with the real MusicaService and mocked AlbumService
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
            Assert.Equal(2, model.Count());
        }

        [Fact]

        public void Criar_ReturnsViewResult_WithAllAlbumsInRepository()
        {
            var result = _controller.Criar();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<MusicaViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Albuns.Count());
        }
    }
}
