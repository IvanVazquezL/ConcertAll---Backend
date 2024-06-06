using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
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

        public async Task<List<GenreResponseDto>> GetAsync()
        {
            var items = await context.Genres
                .AsNoTracking()
                .ToListAsync();

            //  Mapping
            var genresResponseDto = items.Select(item => new GenreResponseDto{
                Id = item.Id,
                Name = item.Name,
                Status = item.Status
            }).ToList();
            return genresResponseDto;
        }
        public async Task<GenreResponseDto?> GetAsync(int id)
        {
            var item = await context.Genres
              //.FindAsync(id);
                .AsNoTracking()
                .FirstOrDefaultAsync(genre => genre.Id == id);

            var genreResponseDto = new GenreResponseDto();

            if (item is not null)
            {
                //  Mapping
                genreResponseDto.Id = item.Id;
                genreResponseDto.Name = item.Name;
                genreResponseDto.Status = item.Status;
            }
            else
                throw new InvalidOperationException($"Couldn't find a record with id {id}");
            return genreResponseDto;
        }

        public async Task<int> AddAsync(GenreRequestDto genreRequestDto)
        {
            //  Mapping
            var genre = new Genre
            {
                Name = genreRequestDto.Name,
                Status = genreRequestDto.Status,
            };

            context.Genres.Add(genre);
            await context.SaveChangesAsync();

            return genre.Id;
        }

        public async Task UpdateAsync(int id, GenreRequestDto genreRequestDto)
        {
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(genre => genre.Id == id);

            if (item is not null)
            {
                item.Name = genreRequestDto.Name;
                item.Status = genreRequestDto.Status;

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
            var item = await context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(genre => genre.Id == id);

            if (item is not null)
            {
                context.Genres.Remove(item);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Couldn't find a record with id {id}");
            }
        }

    }
}
