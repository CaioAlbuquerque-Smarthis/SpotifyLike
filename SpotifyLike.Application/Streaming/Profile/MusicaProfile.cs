using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace Spotify.Application.Streaming.Profile
{
    public class MusicaProfile : AutoMapper.Profile
    {
        public MusicaProfile()
        {
            CreateMap<MusicaDto, Musica>()
                .ReverseMap();
        }
    }
}