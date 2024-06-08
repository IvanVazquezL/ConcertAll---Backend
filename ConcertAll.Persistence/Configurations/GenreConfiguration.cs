using ConcertAll.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(genre => genre.Name).HasMaxLength(50);
            builder.ToTable(nameof(Genre), "Musicals");
        }
    }
}
