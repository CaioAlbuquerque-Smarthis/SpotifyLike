﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Transacao.ValueObject;

namespace SpotifyLike.Domain.Transacao.Aggregates
{
    public class Transacao
    {

        public Guid Id { get; set; }
        public DateTime DtTransacao { get; set; }
        public Monetario Valor { get; set; }
        public String Descricao { get; set; }
        public Merchant Merchant { get; set; }
        //Coloco virtual no merchant?
        //public virtual Usuario UsuarioDestino { get; set; }



     }
}
