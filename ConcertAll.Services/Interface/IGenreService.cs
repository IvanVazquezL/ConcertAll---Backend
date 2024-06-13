using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;

namespace ConcertAll.Services.Interface
{
    public interface IGenreService
    {
        Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync();
        Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id);
        Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request);
    }
}