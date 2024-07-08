using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Dto
{
    public class MusicaRelatorioDto
    {
        public Guid MusicaId { get; set; }
        public String NomeMusica { get; set; }
        public int QuantidadeCurtidas { get; set; }
    }
}
