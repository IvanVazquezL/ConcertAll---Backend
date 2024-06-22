using ConcertAll.Entities;

namespace ConcertAll.Repositories
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollbackAsync();
    }
}
