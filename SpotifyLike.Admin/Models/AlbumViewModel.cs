using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Models
{
    public class AlbumViewModel
    {
        public IEnumerable<BandaDto> Bandas { get; set; }
        public AlbumDto AlbumDto { get; set; }
    }
}
