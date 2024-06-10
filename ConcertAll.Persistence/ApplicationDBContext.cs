using ConcertAll.Entities;
using ConcertAll.Entities.Info;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ConcertInfo>().HasNoKey();
            //modelBuilder.Entity<Genre>().Property(genre => genre.Name).HasMaxLength(50);
        }

        //  Entities to tables
        //public DbSet<Genre> Genres { get; set; }
    }
}
