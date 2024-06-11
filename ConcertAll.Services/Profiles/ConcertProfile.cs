using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using ConcertAll.Entities.Info;

namespace ConcertAll.Services.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            //           origin,       destination
            CreateMap<ConcertInfo, ConcertResponseDto>();
            CreateMap<Concert, ConcertResponseDto>();
            CreateMap<ConcertRequestDto, Concert>()
                .ForMember(
                destination => destination.DateEvent,
                origin => origin.MapFrom(
                    concert => Convert.ToDateTime($"{concert.DateEvent} {concert.TimeEvent}"
                ))
            );
        }

    }
}
