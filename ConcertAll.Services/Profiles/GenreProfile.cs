using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Services.Profiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreResponseDto>();
            CreateMap<GenreRequestDto, Genre>();

        }
    }
}
