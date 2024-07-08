using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Spotify.Application.Streaming;
using SpotifyLike.Application.Admin.Dto;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Admin
{
    public class RelatorioBandaService
    {
        private BandaService BandaService { get; set; }
        private UsuarioService UsuarioService { get; set; }
        private IMapper Mapper { get; set; }
        public RelatorioBandaService(BandaService bandaService
                                     ,UsuarioService usuarioService ,IMapper mapper)
        {
            this.BandaService= bandaService;
            this.UsuarioService = usuarioService;
            this.Mapper = mapper;
        }

        public IEnumerable<BandaRelatorioDto> GerarRelatorioBandasFavoritas()
        {
            // Obter todos os usuários com suas playlists favoritas
            var musicasFavoritas = this.UsuarioService.GerarRelatorioMusicasFavoritas();

            // Criar um dicionário para contar as ocorrências das músicas
            var dicionarioContagem = new Dictionary<Guid, BandaRelatorioDto>();

            foreach (var musica in musicasFavoritas)
            {
                var bandaFavoritaDto = this.BandaService.BuscarPorMusicaId(musica.MusicaId);

                if (bandaFavoritaDto != null)
                {
                    if (dicionarioContagem.ContainsKey(bandaFavoritaDto.Id))
                    {
                        dicionarioContagem[bandaFavoritaDto.Id].QuantidadeCurtidas+=musica.QuantidadeCurtidas;
                    }
                    else
                    {
                        dicionarioContagem[bandaFavoritaDto.Id] = new BandaRelatorioDto
                        {
                            BandaId = bandaFavoritaDto.Id,
                            NomeBanda = bandaFavoritaDto.Nome,
                            QuantidadeCurtidas = musica.QuantidadeCurtidas
                        };
                    }
                }
                
            }

            return dicionarioContagem.Values.OrderByDescending(m => m.QuantidadeCurtidas);
        }
    }
}
