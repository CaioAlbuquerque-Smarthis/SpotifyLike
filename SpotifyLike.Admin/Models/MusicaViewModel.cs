using SpotifyLike.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Models
{
    public class MusicaViewModel
    {
        public IEnumerable<BandaDto> Bandas { get; set; }
        public String BandaId { get; set; }
        public String NomeAlbum { get; set; }
        public MusicaDto MusicaDto { get; set; }
    }
}
