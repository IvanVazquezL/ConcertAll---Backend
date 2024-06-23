using ConcertAll.Dto.Request;
using ConcertAll.Entities;
using ConcertAll.Entities.Info;
using ConcertAll.Persistence;
using ConcertAll.Repositories.Utils;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor httpContext;

        public ConcertRepository(ApplicationDBContext context, IHttpContextAccessor httpContext) : base(context) 
        {
            this.httpContext = httpContext;
        }

        public override async Task<ICollection<Concert>> GetAsync()
        {
            //  eager loading
            return await context.Set<Concert>()
                .Include(concert => concert.Genre)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<ConcertInfo>> GetAsync(string? title, PaginationDto pagination)
        {
            //optimized eager loading 
            var queryable = context.Set<Concert>()
                .Include(concert => concert.Genre)
                .Where(concert => concert.Title.Contains(title ?? string.Empty))
                .IgnoreQueryFilters()
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
                .AsQueryable();

            await httpContext.HttpContext.InsertPaginationHeader(queryable);
            return await queryable.OrderBy(x => x.Id).Paginate(pagination).ToListAsync();
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
