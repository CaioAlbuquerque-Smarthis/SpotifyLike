using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository.Repository;

namespace SpotifyLike.Application.Streaming
{
    public class MusicaService
    {
        private MusicaRepository MusicaRepository { get; set; }
        private IMapper Mapper { get; set; }

        public MusicaService(MusicaRepository musicaRepository, IMapper mapper) 
        {
            MusicaRepository = musicaRepository;
            Mapper = mapper;
        }
        public IEnumerable<MusicaDto> Obter()
        {
            var musica = this.MusicaRepository.GetAll();
            return this.Mapper.Map<IEnumerable<MusicaDto>>(musica);
        }

        public void AdicionarEmAlbum(Guid IdAlbum, MusicaDto musicaDto)
        {

        }
    }
}
