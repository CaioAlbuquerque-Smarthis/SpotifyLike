﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Streaming.ValueObject;

namespace SpotifyLike.Application.Streaming.Dto
{
    public class MusicaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Duracao Duracao { get; set; }

        public MusicaDto()
        {
            Duracao = new Duracao(0); 
        }
    }
}
