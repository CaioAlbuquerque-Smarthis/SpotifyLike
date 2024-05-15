using System;
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
using SpotifyLike.Repository.Mapping.Admin;

namespace SpotifyLike.Repository
{
    public class SpotifyLikeAdminContext : DbContext
    {
        public DbSet<UsuarioAdmin> UsuarioAdministradores { get; set; }


        public SpotifyLikeAdminContext(DbContextOptions<SpotifyLikeAdminContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UsuarioAdministradorMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
