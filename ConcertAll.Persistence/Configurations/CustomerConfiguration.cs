using ConcertAll.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConcertAll.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(customer => customer.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(customer => customer.FullName)
                .HasMaxLength(200);

            builder.ToTable(nameof(Customer), "Musicals");

        }
    }
}
