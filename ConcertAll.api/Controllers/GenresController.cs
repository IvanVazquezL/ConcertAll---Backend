using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConcertAll.Api.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenresController> logger;

        public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();

            try
            {
                var genresDb = await repository.GetAsync();

                //  Mapping
                var genres = genresDb.Select(genre => new GenreResponseDto
                {
                    Id = genre.Id,
                    Name = genre.Name,
                    Status = genre.Status
                }).ToList();

                response.Data = genres;
                response.Success = true;
                logger.LogInformation("Retrieving all genres");

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving all genres";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();
            var genreDb = await repository.GetAsync(id);

            try
            {
                if (genreDb is null)
                {
                    logger.LogWarning($"Genre with id {id} wat not found");
                    return NotFound(response);
                }
                
                //  Mapping
                var genre = new GenreResponseDto
                {
                    Id = genreDb.Id,
                    Name = genreDb.Name,
                    Status = genreDb.Status
                };

                response.Data = genre;
                response.Success = true;
                logger.LogInformation($"Retrieving a genre with id {id}");
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving a genre";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDto genreRequestDto)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                //  Mapping
                var genreDb = new Genre()
                {
                    Name = genreRequestDto.Name,
                    Status = genreRequestDto.Status
                };

                var genreId = await repository.AddAsync(genreDb);
                response.Data = genreId;
                response.Success = true;
                logger.LogInformation($"Posting a genre with id {genreId}");

                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while posting a genre";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
                
                return BadRequest(response);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDto genreRequestDto)
        {
            var response = new BaseResponse();

            try
            {
                var genreDb = await repository.GetAsync(id);

                if (genreDb is null)
                {
                    response.ErrorMessage = "Record not found";
                    return NotFound(response);
                }

                genreDb.Name = genreRequestDto.Name;
                genreDb.Status = genreRequestDto.Status;

                await repository.UpdateAsync();
                response.Success = true;
                logger.LogInformation($"Updating a genre with id {id}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while updating a genre";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new BaseResponse();

            try
            {
                var genreDb = await repository.GetAsync(id);
                
                if (genreDb is null)
                {
                    response.ErrorMessage = "Record not found";
                    return NotFound(response);
                }

                await repository.DeleteAsync(id);
                response.Success = true;
                logger.LogInformation($"Deleting a genre with id {id}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while deleting a genre";
                logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");

                return BadRequest(response);
            }
        }
    }
}
