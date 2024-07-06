using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SpotifyLike.Admin.Controllers;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;
using SpotifyLike.Repository.Repository;
using SpotifyLike.Repository;
using Spotify.Application.Streaming;
using Spotify.Application.Streaming.Profile;
using SpotifyLike.Application.Streaming.Profile;

namespace SpotifyLike.Tests.Admin
{
    public class AlbumTests
    {
        private readonly BandaService _bandaService;
        private readonly AlbumController _controller;
        private readonly AlbumService _albumService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMapper _mapper;
        private readonly AlbumViewModel _albumViewModel;

        public AlbumTests()
        {
            // Create a DbContextOptions with an in-memory database
            var options = new DbContextOptionsBuilder<SpotifyLikeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BandaProfile>();
                cfg.AddProfile<AlbumProfile>();
            });

            _mapper = config.CreateMapper();

            var context = new SpotifyLikeContext(options);

            var bandas = new List<Banda>
            {
                new Banda { Id = new Guid() , Nome = "Banda1", Backdrop = "Backdrop", Descricao = "Descrição"},
                new Banda { Id = new Guid(), Nome = "Banda2", Backdrop = "Backdrop", Descricao = "Descrição" }
            };
            context.Bandas.AddRange(bandas);
            context.SaveChanges();

            // Instantiate the repository with the instantiated context
            var bandaRepository = new BandaRepository(context);

            var albuns = new List<Album>
            {
                new Album { Id = new Guid(), Nome = "Album1", Musica = new List<Musica>() },
                new Album { Id = new Guid(), Nome = "Album2", Musica = new List<Musica>() }
            };
            context.Albuns.AddRange(albuns);
            context.SaveChanges();

            var albumRepository = new AlbumRepository(context);

            _mapperMock = new Mock<IMapper>();

            var albunsDtos = new List<AlbumDto>
            {
                new AlbumDto { Nome = "Album1", Musicas = new List<MusicaDto>() },
                new AlbumDto { Nome = "Album2", Musicas = new List<MusicaDto>() }
            };
            _mapperMock.Setup(m => m.Map<IEnumerable<AlbumDto>>(It.IsAny<IEnumerable<Album>>())).Returns(albunsDtos);

            _bandaService = new BandaService(bandaRepository, _mapper);

            _albumService = new AlbumService(albumRepository, _mapper);

            _controller = new AlbumController(_albumService, _bandaService);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfAlbumDtos()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AlbumDto>>(viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        [Fact]

        public void Criar_ReturnsViewResult_WithAllBandasInRepository()
        {
            var result = _controller.Criar();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AlbumViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Bandas.Count());
        }
    }
}
