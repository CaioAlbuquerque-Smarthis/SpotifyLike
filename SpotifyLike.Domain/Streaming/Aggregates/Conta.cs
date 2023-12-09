using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Conta
    {
        public Guid Id { get; set; }
        public String Nome { get; set; }
        public String CPF { get; set; }
        public String Email { get; set; }
        public String Endereco { get; set; }
        public Plano Plano { get; set; }

        public List<Album> Playlists { get; set; }

    }
}
