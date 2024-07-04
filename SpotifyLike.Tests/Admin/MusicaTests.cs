using System;
using SpotifyLike.Admin.Controllers;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.ValueObject;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyLike.Tests.Admin
{
    public class MusicaTests
    {
        private readonly Mock<MusicaService> _musicaServiceMock;
        private readonly Mock<AlbumService> _albumServiceMock;
        private readonly MusicaController _controller;

        public MusicaTests()
        {
            _musicaServiceMock = new Mock<MusicaService>();
            _controller = new MusicaController(_musicaServiceMock.Object, _albumServiceMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult_WithAListOfMusicaDtos()
        {
            // Arrange
            var musicaDtos = new List<MusicaDto>
            {
                new MusicaDto { Nome = "Song1", Duracao = new Duracao(200) },
                new MusicaDto { Nome = "Song2", Duracao = new Duracao(300) }
            };
            _musicaServiceMock.Setup(service => service.Obter()).Returns(musicaDtos);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MusicaDto>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
