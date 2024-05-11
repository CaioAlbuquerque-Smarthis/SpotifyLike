using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Admin.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Mapping.Admin
{
    public class UsuarioAdministradorMapping : IEntityTypeConfiguration<UsuarioAdministrador>
    {
        public void Configure(EntityTypeBuilder<UsuarioAdministrador> builder)
        {
            builder.ToTable(nameof(UsuarioAdministrador));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Perfil).IsRequired();
        }
    }
}
