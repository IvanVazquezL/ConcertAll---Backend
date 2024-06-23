
using AutoMapper;
using Azure.Core;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
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

        public async Task<BaseResponseGeneric<ICollection<ConcertResponseDto>>> GetAsync(string? title, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<ConcertResponseDto>>();

            try
            {
                var data = await repository.GetAsync(title, pagination);

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

        public async Task<BaseResponseGeneric<ConcertResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<ConcertResponseDto>();

            try
            {
                var data = await repository.GetAsync(id);

                response.Data = mapper.Map<ConcertResponseDto>(data);
                response.Success = data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(ConcertRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                response.Data = await repository.AddAsync(mapper.Map<Concert>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while saving data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ConcertRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var data = await repository.GetAsync(id);

                if (data is null)
                {
                    response.ErrorMessage = $"Record with {id} was not found";
                    return response;
                }

                mapper.Map(request, data);
                await repository.UpdateAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while updating data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while deleting data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> FinalizeAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await repository.FinalizeAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while finalizing concert";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
