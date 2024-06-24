using ConcertAll.Dto.Request;
using ConcertAll.Entities;
using ConcertAll.Persistence;
using ConcertAll.Repositories.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace ConcertAll.Repositories
{
    public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SaleRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateTransactionAsync()
        {
            await context.Database.BeginTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await context.Database.RollbackTransactionAsync();
        }

        public override async Task<int> AddAsync(Sale entity)
        {
            entity.SaleDate = DateTime.Now;
            var nextNumber = await context.Set<Sale>().CountAsync() + 1;
            entity.OperationNumber = $"{nextNumber:000000}";
            
            await context.AddAsync(entity);
            return entity.Id;
        }

        public override async Task UpdateAsync()
        {
            await context.Database.CommitTransactionAsync();
            await base.UpdateAsync();
        }

        public override async Task<Sale?> GetAsync(int id)
        {
            return await context.Set<Sale>()
                .Include(sale => sale.Customer)
                .Include(sale => sale.Concert)
                .ThenInclude(sale => sale.Genre)
                .Where(sale => sale.Id == id)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate, Expression<Func<Sale, TKey>> orderBy, PaginationDto pagination)
        {
            var queryable = context.Set<Sale>()
                .Include(x => x.Customer)
                .Include(x => x.Concert)
                .ThenInclude(x => x.Genre)
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .AsQueryable();

            await httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);
            var response = await queryable.Paginate(pagination).ToListAsync();
            return response;
        }
    }
}
