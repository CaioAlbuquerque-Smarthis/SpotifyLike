using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Conta.ValueObject;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Cartao
    {
        public Guid Id { get; set; }
        public Limite Valor { get; set; }
        public Boolean Ativo { get; set; }

    }
}
