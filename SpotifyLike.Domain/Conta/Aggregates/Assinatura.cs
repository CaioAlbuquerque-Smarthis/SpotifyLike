using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public Plano Plano { get; set; }
        public DateTime DtAtivacao{ get; set; }
        public Boolean Ativo { get; set; }

        public DateTime DtVencimento { get; set; }

    }
}
