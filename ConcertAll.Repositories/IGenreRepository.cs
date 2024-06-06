using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;

namespace ConcertAll.Repositories
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(GenreRequestDto genre);
        Task DeleteAsync(int id);
        Task<List<GenreResponseDto>> GetAsync();
        Task<GenreResponseDto?> GetAsync(int id);
        Task UpdateAsync(int id, GenreRequestDto genre);
    }
}