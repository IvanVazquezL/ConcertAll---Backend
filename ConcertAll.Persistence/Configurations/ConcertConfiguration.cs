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
    public class ConcertConfiguration : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            builder.Property(concert => concert.Title).HasMaxLength(100);
            builder.Property(concert => concert.Description).HasMaxLength(100);
            builder.Property(concert => concert.Place).HasMaxLength(100);
            builder.Property(concert => concert.DateEvent)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");
            builder.Property(concert => concert.ImageUrl)
                .HasMaxLength(250)
                .IsUnicode(false);
        }
    }
}
