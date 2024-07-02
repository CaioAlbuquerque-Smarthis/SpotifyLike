using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SpotifyLike.Application.Streaming.Dto;
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
    }
}
