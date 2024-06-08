using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConcertAll.Repositories
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDBContext context) : base(context)
        {

        }
    }
}
