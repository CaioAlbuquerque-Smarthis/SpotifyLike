using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Admin.Dto
{
    public class BandaRelatorioDto
    {
        public Guid BandaId { get; set; }
        public String NomeBanda { get; set; }
        public int QuantidadeCurtidas { get; set; }
    }
}
