using ConcertAll.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertAll.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(sale => sale.OperationNumber)
                .IsUnicode(false)
                .HasMaxLength(20);
            builder.Property(sale => sale.SaleDate)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");
            builder.Property(sale => sale.Total)
                .HasColumnType("decimal(10,2)");
            builder.ToTable(nameof(Sale), "Musicals");
        }
    }
}
