using ConcertAll.Entities;
using ConcertAll.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConcertAll.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;
        public GenresController(IGenreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Genre>>> Get()
        {
            var data = await repository.GetAsync();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var data = await repository.GetAsync(id);
            return data is not null ? Ok(data) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> Post(Genre genre)
        {
            await repository.AddAsync(genre);
            return Ok(genre);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Genre genre)
        {
            await repository.UpdateAsync(id, genre);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }
    }
}
