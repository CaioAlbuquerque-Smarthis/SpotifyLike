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
    public class AlbumService
    {
        private AlbumRepository AlbumRepository { get; set; }
        private IMapper Mapper { get; set; }
        public AlbumService(AlbumRepository albumRepository, IMapper mapper)
        {
            AlbumRepository = albumRepository;
            Mapper = mapper;
        }

        public IEnumerable<AlbumDto> Obter()
        {
            var album = this.AlbumRepository.GetAll();
            return this.Mapper.Map<IEnumerable<AlbumDto>>(album);
        }

        public AlbumDto Obter(Guid id)
        {
            var album = this.AlbumRepository.GetById(id);
            return this.Mapper.Map<AlbumDto>(album);
        }

        public MusicaDto AssociarMusica(MusicaDto dto, Guid albumId)
        {
            var album = this.AlbumRepository.GetById(albumId);

            if (album == null)
            {
                throw new Exception("Álbum não encontrado");
            }

            var musica = this.Mapper.Map<Musica>(dto);

            album.AdicionarMusica(musica);

            this.AlbumRepository.Update(album);

            var result = dto;

            return result;

        }
    }
}
