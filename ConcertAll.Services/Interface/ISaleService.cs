using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;

namespace ConcertAll.Services.Interface
{
    public interface ISaleService
    {
        Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request);
        Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id);
        Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(SaleByDateSearchDto search, PaginationDto pagination);
        Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(string email, string title, PaginationDto pagination);
    }
}
