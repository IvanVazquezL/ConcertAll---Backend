using ConcertAll.Entities;

namespace ConcertAll.Repositories
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<Customer?> GetByEmailAsync(string email);

    }
}
