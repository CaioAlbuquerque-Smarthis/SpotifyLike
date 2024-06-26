﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpotifyLike.Domain.Admin.Aggregates;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Notificacao.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Domain.Transacao.Aggregates;

namespace SpotifyLike.Repository
{
    public class SpotifyLikeContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Assinatura> Assinaturas{ get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Notificacao> Notificacoes{ get; set; }
        public DbSet<Cartao> Cartoes{ get; set; }
        public DbSet<Transacao> Transacao{ get; set; }
        public DbSet<Banda> Bandas { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Album> Albuns { get; set; }


        public SpotifyLikeContext(DbContextOptions<SpotifyLikeContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpotifyLikeContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
