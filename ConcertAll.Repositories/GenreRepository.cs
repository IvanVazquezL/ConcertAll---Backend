using ConcertAll.Entities;
using ConcertAll.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertAll.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDBContext context;

        public GenreRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Genre>> GetAsync()
        {
            return await context.Genres
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Genre?> GetAsync(int id)
        {
            var item = await context.Genres
              //.FindAsync(id);
                .AsNoTracking()
                .FirstOrDefaultAsync(genre => genre.Id == id);

            if (item is not null)
                return item;
            else
                throw new InvalidOperationException($"Couldn't find a record with id {id}");
        }

        public async Task AddAsync(Genre genre)
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Genre genre)
        {
            var item = await GetAsync(id);

            if (item is not null)
            {
                item.Name = genre.Name;
                item.Status = genre.Status;

                context.Update(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Couldn't find a record with id {id}");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);

            if (item is not null)
            {
                context.Genres.Remove(item);
                await context.SaveChangesAsync();
            }
        }

    }
}
