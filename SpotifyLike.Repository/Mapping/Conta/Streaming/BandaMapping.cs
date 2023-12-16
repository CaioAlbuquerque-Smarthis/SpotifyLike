using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Repository.Mapping.Conta.Streaming
{
    public class BandaMapping : IEntityTypeConfiguration<Banda>
    {
        public void Configure(EntityTypeBuilder<Banda> builder)
        {
            builder.ToTable(nameof(Banda));

        }
    }
}
