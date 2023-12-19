using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.ValueObject;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Musica
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public Duracao Duracao { get; set; }

        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

    }
}
