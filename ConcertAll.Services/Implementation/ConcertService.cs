
using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Repositories;
using ConcertAll.Services.Interface;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ConcertAll.Services.Implementation
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository repository;
        private readonly ILogger<ConcertService> logger;
        private readonly IMapper mapper;

        public ConcertService(IConcertRepository repository, ILogger<ConcertService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<ConcertResponseDto>>> GetAsync(string? title)
        {
            var response = new BaseResponseGeneric<ICollection<ConcertResponseDto>>();

            try
            {
                var data = await repository.GetAsync(title);

                response.Data = mapper.Map<ICollection<ConcertResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public Task<BaseResponseGeneric<ConcertResponseDto>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseGeneric<int>> AddAsync(ConcertRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> FinalizeAsync(int id)
        {
            throw new NotImplementedException();
        }





        public Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
