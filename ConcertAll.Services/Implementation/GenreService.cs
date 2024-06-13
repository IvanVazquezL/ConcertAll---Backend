using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Repositories;
using ConcertAll.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository repository;
        private readonly ILogger<GenreService> logger;
        private readonly IMapper mapper;

        public GenreService(IGenreRepository repository, ILogger<GenreService> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync()
        {
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();

            try
            {
                var data = await repository.GetAsync();

                response.Data = mapper.Map<ICollection<GenreResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<GenreResponseDto>();

            try
            {
                var data = await repository.GetAsync(id);

                response.Data = mapper.Map<GenreResponseDto>(data);
                response.Success = data != null;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                response.Data = await repository.AddAsync(mapper.Map<Genre>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while adding data";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request)
        {
            var response = new BaseResponse();

            try
            {
                var genre = await repository.GetAsync(id);

                if (genre is null)
                {
                    response.ErrorMessage = $"Genre with id {id} doesn't exist.";
                    return response;
                }

                mapper.Map(request, genre);
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
    }
}
