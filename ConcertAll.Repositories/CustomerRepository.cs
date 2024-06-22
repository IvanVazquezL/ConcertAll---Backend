using ConcertAll.Entities;
using ConcertAll.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertAll.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDBContext context) : base(context)
        {
            
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await context.Set<Customer>()
                .FirstOrDefaultAsync(customer => customer.Email == email);
        }
    }
}
