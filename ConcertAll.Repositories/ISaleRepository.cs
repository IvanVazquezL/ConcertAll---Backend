using ConcertAll.Dto.Request;
using ConcertAll.Entities;
using System.Linq.Expressions;

namespace ConcertAll.Repositories
{
    public interface ISaleRepository : IRepositoryBase<Sale>
    {
        Task CreateTransactionAsync();
        Task RollbackAsync();
        Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate, Expression<Func<Sale, TKey>> orderBy, PaginationDto pagination);

    }
}
