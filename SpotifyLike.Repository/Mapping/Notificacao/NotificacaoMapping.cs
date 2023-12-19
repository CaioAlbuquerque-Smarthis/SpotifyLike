using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Core.ValueObject;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Mapping.Notificacao
{
    public class NotificacaoMapping : IEntityTypeConfiguration<Domain.Notificacao.Aggregates.Notificacao>
    {
        public void Configure(EntityTypeBuilder<Domain.Notificacao.Aggregates.Notificacao> builder)
        {
            builder.ToTable(nameof(Domain.Notificacao.Aggregates.Notificacao));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Mensagem).IsRequired().HasMaxLength(250);
            builder.Property(x => x.DtNotificacao).IsRequired();
            builder.Property(x => x.TipoNotificacao).IsRequired();

            builder.HasOne(x => x.UsuarioDestino).WithMany(x => x.Notificacoes).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.UsuarioRemetente).WithMany().IsRequired(false);

        }
    }
}
