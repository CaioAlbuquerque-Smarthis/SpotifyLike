using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Boolean Publica { get; set; }
        public Usuario Usuario { get; set; }
        public List<Musica> Musicas { get; set; }
        public DateTime DtCriacao { get; set; }
        public Boolean Favorita { get; set; }

        public void AdicionarMusica(Musica musica) =>
            this.Musicas.Add(musica);
        public void AdicionarMusicas(List<Musica> musica) =>
            this.Musicas.AddRange(musica);
    }
}
