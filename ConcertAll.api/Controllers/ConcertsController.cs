using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ConcertAll.Api.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly ILogger<ConcertsController> logger;

        public ConcertsController(IConcertRepository repository, IGenreRepository genreRepository, ILogger<ConcertsController> logger)
        {
            this.repository = repository;
            this.genreRepository = genreRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = new BaseResponseGeneric<ICollection<ConcertResponseDto>>();
            var concertsDb = await repository.GetAsync();

            //  Mapping
            var concerts = concertsDb.Select(concert => new ConcertResponseDto
            {
                Title = concert.Title,
                Description = concert.Description,
                Place = concert.Place,
                UnitPrice = concert.UnitPrice,
                GenreId = concert.GenreId,
                DateEvent = concert.DateEvent,
                ImageUrl = concert.ImageUrl,
                TicketsQuantity = concert.TicketsQuantity,
                Finalized = concert.Finalized
            }).ToList();

            response.Data = concerts;
            response.Success = true;

            return Ok(response);
        }

        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var concerts = await repository.GetAsync(
                concert => concert.Title.Contains(title ?? string.Empty),
                concert => concert.DateEvent
            );
            return Ok(concerts);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConcertRequestDto concertRequestDto)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                //  Validate genre id
                var genre = await genreRepository.GetAsync(concertRequestDto.GenreId);

                if (genre is null)
                {
                    response.ErrorMessage = $"genreId {concertRequestDto.GenreId} could not been found";
                    logger.LogWarning(response.ErrorMessage);
                    return BadRequest(response);
                }

                //  Mapping 
                var concertDb = new Concert
                {
                    Title = concertRequestDto.Title,
                    Description = concertRequestDto.Description,
                    Place = concertRequestDto.Place,
                    UnitPrice = concertRequestDto.UnitPrice,
                    GenreId = concertRequestDto.GenreId,
                    DateEvent = concertRequestDto.DateEvent,
                    ImageUrl = concertRequestDto.ImageUrl,
                    TicketsQuantity = concertRequestDto.TicketsQuantity
                };

                response.Data = await repository.AddAsync(concertDb);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error happened while retrieving the information";
                logger.LogError(ex, ex.Message );
            }

            return Ok(response);
        }
    }
}
