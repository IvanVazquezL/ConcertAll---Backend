using AutoMapper;
using ConcertAll.Dto.Response;
using ConcertAll.Entities.Info;

namespace ConcertAll.Services.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            //           origin,       destination
            CreateMap<ConcertInfo, ConcertResponseDto>();


        }

    }
}
