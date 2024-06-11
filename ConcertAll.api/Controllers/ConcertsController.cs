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
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await service.GetAsync(id);
            if (response.Data is null) return NotFound(response);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConcertRequestDto concertRequestDto)
        {
            var response = await service.AddAsync(concertRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ConcertRequestDto concertRequestDto)
        {
            var response = await service.UpdateAsync(id, concertRequestDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id)
        {
            var response = await service.FinalizeAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
