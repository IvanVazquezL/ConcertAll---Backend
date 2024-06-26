using ConcertAll.Entities;
using ConcertAll.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConcertAll.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<ConcertAllUserIdentity>
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

            modelBuilder.Entity<ConcertAllUserIdentity>(x => x.ToTable("User"));
            modelBuilder.Entity<IdentityRole>(x => x.ToTable("Role"));
            modelBuilder.Entity<IdentityUserRole<string>>(x => x.ToTable("UserRole"));
        }

        //  Entities to tables
        //public DbSet<Genre> Genres { get; set; }
    }
}
