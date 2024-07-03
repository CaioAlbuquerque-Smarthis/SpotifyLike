using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Models
{
    public class MusicaViewModel
    {
        public IEnumerable<AlbumDto> Albuns { get; set; }
        public Guid AlbumId { get; set; }
        public MusicaDto MusicaDto { get; set; }
    }
}
