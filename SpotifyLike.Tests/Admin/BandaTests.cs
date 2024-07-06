using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Spotify.Application.Streaming;
using SpotifyLike.Admin.Controllers;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;
using SpotifyLike.Repository;
using Spotify.Application.Streaming.Profile;

namespace SpotifyLike.Tests.Admin
{
    public class BandaTests
    {
        private readonly BandaService _bandaService;
        private readonly BandaController _controller;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IMapper _mapper;

        public BandaTests()
        {
            // Create a DbContextOptions with an in-memory database
            var options = new DbContextOptionsBuilder<SpotifyLikeContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BandaProfile>();
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

            _bandaService = new BandaService(bandaRepository, _mapper);


            _controller = new BandaController(_bandaService);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfBandaDtos()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BandaDto>>(viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        [Fact]

        public void Criar_ReturnsViewResult_WithAllBandasInRepository()
        {
            var result = _controller.Criar();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
