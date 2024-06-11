using ConcertAll.Entities;
using ConcertAll.Entities.Info;
using ConcertAll.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Repositories
{
    public class ConcertRepository : RepositoryBase<Concert>, IConcertRepository
    {
        public ConcertRepository(ApplicationDBContext context) : base(context) 
        {
            
        }

        public override async Task<ICollection<Concert>> GetAsync()
        {
            //  eager loading
            return await context.Set<Concert>()
                .Include(concert => concert.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<ConcertInfo>> GetAsync(string? title)
        {
            /*
            //optimized eager loading 
            return await context.Set<Concert>()
                .Include(concert => concert.Genre)
                .Where(concert => concert.Title.Contains(title ?? string.Empty))
                .AsNoTracking()
                .Select(concert => new ConcertInfo
                {
                    Id = concert.Id,
                    Title = concert.Title,
                    Description = concert.Description,
                    Place = concert.Place,
                    UnitPrice = concert.UnitPrice,
                    Genre = concert.Genre.Name,
                    GenreId = concert.GenreId,
                    DateEvent = concert.DateEvent.ToShortDateString(),
                    TimeEvent = concert.DateEvent.ToShortTimeString(),
                    ImageUrl = concert.ImageUrl,
                    TicketsQuantity = concert.TicketsQuantity,
                    Finalized = concert.Finalized,
                    Status = concert.Finalized ? "Active" : "Inactive"
                })
                .ToListAsync();
            */

            /*
            //  lazy loading
            return await context.Set<Concert>()
                .Where(concert => concert.Title.Contains(title ?? string.Empty))
                .AsNoTracking()
                .Select(concert => new ConcertInfo
                {
                    Id = concert.Id,
                    Title = concert.Title,
                    Description = concert.Description,
                    Place = concert.Place,
                    UnitPrice = concert.UnitPrice,
                    Genre = concert.Genre.Name,
                    GenreId = concert.GenreId,
                    DateEvent = concert.DateEvent.ToShortDateString(),
                    TimeEvent = concert.DateEvent.ToShortTimeString(),
                    ImageUrl = concert.ImageUrl,
                    TicketsQuantity = concert.TicketsQuantity,
                    Finalized = concert.Finalized,
                    Status = concert.Finalized ? "Active" : "Inactive"
                })
                .ToListAsync();
            */

            //  raw queries
            var query = context.Set<ConcertInfo>().
                FromSqlRaw("usp_ListConcerts {0}", title ?? string.Empty);
            return await query.ToListAsync();
        }

        public async Task FinalizeAsync(int id)
        {
            var concert = await GetAsync(id);

            if (concert is not null)
            {
                concert.Finalized = true;
                await UpdateAsync();
            }
        }
    }
}
