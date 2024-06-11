using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Repositories;
using ConcertAll.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ConcertAll.Api.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService service;

        public ConcertsController(IConcertService service)
        {
            this.service = service;
        }

        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var response = await service.GetAsync(title);
            return response.Success ? Ok(response): BadRequest(response);
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
