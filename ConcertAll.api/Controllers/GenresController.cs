using ConcertAll.Entities;
using ConcertAll.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConcertAll.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly GenreRepository repository;
        public GenresController(GenreRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = repository.Get();
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Genre> Get(int id)
        {
            var data = repository.Get(id);
            return data is not null ? Ok(data) : NotFound();
        }

        [HttpPost]
        public ActionResult<Genre> Post(Genre genre)
        {
            repository.Add(genre);
            return Ok(genre);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Genre genre)
        {
            repository.Update(id, genre);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}
