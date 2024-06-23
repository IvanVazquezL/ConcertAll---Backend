using ConcertAll.Dto.Request;
using ConcertAll.Entities;
using ConcertAll.Entities.Info;

namespace ConcertAll.Repositories
{
    public interface IConcertRepository : IRepositoryBase<Concert>
    {
        Task<ICollection<ConcertInfo>> GetAsync(string? title, PaginationDto pagination);
        Task FinalizeAsync(int id);
    }
}
