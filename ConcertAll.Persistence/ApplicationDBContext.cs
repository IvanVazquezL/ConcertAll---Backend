using ConcertAll.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcertAll.Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        //  Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().Property(genre => genre.Name).HasMaxLength(50);
        }

        //  Entities to tables
        public DbSet<Genre> Genres { get; set; }
    }
}
