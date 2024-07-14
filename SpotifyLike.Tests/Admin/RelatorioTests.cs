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
using Spotify.Application.Streaming.Profile;
using SpotifyLike.Admin.Controllers;
using SpotifyLike.Admin.Models;
using SpotifyLike.Application.Admin;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Application.Streaming;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Application.Streaming.Profile;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Tests.Admin
{
    public class RelatorioTests
    {
        private readonly UsuarioService _usuarioService;
        private readonly RelatorioBandaService _relatorioBandaService;
        private readonly BandaService _bandaService;
        private readonly RelatorioController _controller;
        private readonly IMapper _mapper;
        private readonly MusicaRelatorioDto _musicaRelatorioDto;

        public RelatorioTests()
        {
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

            var usuarios = new List<Usuario>
            {
                new Usuario { Id = new Guid() ,
                              Nome = "Teste",
                              Email = "teste@teste.com",
                              Senha = "123",
                              Playlists = new List<Playlist>
                              {
                                  new Playlist { Id = new Guid() , Favorita = true, Nome = "Teste", Musicas = new List<Musica>
                                                                                                    {
                                                                                                        new Musica { Id = new Guid() , Nome = "Teste"}
                                                                                                    }
                                               }
                              }
                              }
            };

            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            var bandas = new List<Banda>
            {
                new Banda {Id = new Guid(), Nome = "Teste", Backdrop = "Teste", Descricao = "Teste", Albums = new List<Album>
                                                                                                                {
                                                                                                                    new Album {Id = new Guid(),
                                                                                                                               Nome = "Teste",
                                                                                                                               Musica = usuarios.First().Playlists.First().Musicas}
                                                                                                                }
                }
            };

            context.Bandas.AddRange(bandas);
            context.SaveChanges();

            var usuarioRepository = new UsuarioRepository(context);
            var planoRepository = new PlanoRepository(context);
            var bandaRepository = new BandaRepository(context);

            _usuarioService = new UsuarioService(_mapper, usuarioRepository, planoRepository);
            _bandaService = new BandaService(bandaRepository, _mapper);
            _relatorioBandaService = new RelatorioBandaService(_bandaService, _usuarioService, _mapper);
            _controller = new RelatorioController(_usuarioService, _relatorioBandaService);
        }

        [Fact]
        public void Criar_ReturnsViewResult_IEnumerableBandaRelatorioDto()
        {
            // Act
            var result = _controller.RelatorioBanda();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BandaRelatorioDto>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Criar_ReturnsViewResult_IEnumerableMusicaRelatorioDto()
        {
            // Act
            var result = _controller.RelatorioMusica();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<MusicaRelatorioDto>>(viewResult.ViewData.Model);
        }

        
    }

}
