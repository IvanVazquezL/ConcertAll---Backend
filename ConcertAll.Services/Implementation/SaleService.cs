﻿using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Repositories;
using ConcertAll.Services.Interface;
using Microsoft.Extensions.Logging;

namespace ConcertAll.Services.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository repository;
        private readonly ILogger<GenreService> logger;
        private readonly IMapper mapper;
        private readonly IConcertRepository concertRepository;
        private readonly ICustomerRepository customerRepository;

        public SaleService(ISaleRepository repository, ILogger<GenreService> logger, IMapper mapper,
            IConcertRepository concertRepository, ICustomerRepository customerRepository)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.concertRepository = concertRepository;
            this.customerRepository = customerRepository;
        }
        public async Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                await repository.CreateTransactionAsync();
                var entity = mapper.Map<Sale>(request);

                var customer = await customerRepository.GetByEmailAsync(email);

                if (customer is null)
                {
                    customer = new Customer()
                    {
                        Email = request.Email,
                        FullName = request.FullName
                    };

                    customer.Id = await customerRepository.AddAsync(customer);
                }

                entity.CustomerId = customer.Id;

                var concert = await concertRepository.GetAsync(request.ConcertId);

                if (concert is null)
                    throw new Exception($"Concert with {request.ConcertId} id doesn't exist");

                if (DateTime.Today > concert.DateEvent || concert.Finalized)
                    throw new InvalidOperationException(
                        $"Concert {concert.Title} already ended");

                entity.Total = entity.Quantity * (decimal) concert.UnitPrice;

                await repository.AddAsync(entity);
                await repository.UpdateAsync();

                response.Data = entity.Id;
                response.Success = true;

                logger.LogInformation("Sale created succesfully for {email}", email);
            }
            catch(InvalidOperationException ex)
            {
                await repository.RollbackAsync();
                response.ErrorMessage = ex.Message;

                logger.LogError(ex, "{ErrorMessage}", response.ErrorMessage);
            }
            catch (Exception ex)
            {
                await repository.RollbackAsync();
                response.ErrorMessage = "Error while creating the sale";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id)
        {
            var response = new BaseResponseGeneric<SaleResponseDto>();

            try
            {
                var sale = await repository.GetAsync(id);
                response.Data = mapper.Map<SaleResponseDto>(sale);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while retrieving sale";
                logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(SaleByDateSearchDto search, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();

            try
            {
                var dateStart = Convert.ToDateTime(search.DateStart);
                var dateEnd = Convert.ToDateTime(search.DateEnd);

                var data = await repository.GetAsync(
                    predicate: s => s.SaleDate >= dateStart && s.SaleDate <= dateEnd,
                    orderBy: x => x.OperationNumber,
                    pagination
                );

                response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
                response.Success = true;
            } catch(Exception ex)
            {
                response.ErrorMessage = "Error while filtering sales by date";
                logger.LogError(ex, "{ErrorMessage} {Meessage}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(string email, string title, PaginationDto pagination)
        {
            var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();

            try
            {
                var data = await repository.GetAsync(
                    predicate: s => s.Customer.Email == email && s.Concert.Title.Contains(title ?? string.Empty),
                    orderBy: x => x.SaleDate,
                    pagination
                );

                response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error while filtering sales by title";
                logger.LogError(ex, "{ErrorMessage} {Meessage}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
