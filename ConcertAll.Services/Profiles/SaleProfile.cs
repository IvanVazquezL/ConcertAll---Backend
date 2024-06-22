using AutoMapper;
using ConcertAll.Dto.Request;
using ConcertAll.Dto.Response;
using ConcertAll.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Services.Profiles
{
    public class SaleProfile : Profile
    {
        private static readonly CultureInfo culture = new CultureInfo("es-MX");
        public SaleProfile() {
            CreateMap<SaleRequestDto, Sale>()
                .ForMember(destination => destination.Quantity, origin => origin.MapFrom(x => x.TicketsQuantity)
            );
            CreateMap<Sale, SaleResponseDto>()
                .ForMember(destination => destination.SaleId, origin => origin.MapFrom(x => x.Id))
                .ForMember(destination => destination.DateEvent, origin => origin.MapFrom(x => x.Concert.DateEvent.ToString("D", culture)))
                .ForMember(destination => destination.TimeEvent, origin => origin.MapFrom(x => x.Concert.DateEvent.ToString("T", culture)))
                .ForMember(destination => destination.Genre, origin => origin.MapFrom(x => x.Concert.Genre.Name))
                .ForMember(destination => destination.ImageUrl, origin => origin.MapFrom(x => x.Concert.ImageUrl))
                .ForMember(destination => destination.Title, origin => origin.MapFrom(x => x.Concert.Title))
                .ForMember(destination => destination.FullName, origin => origin.MapFrom(x => x.Customer.FullName))
                .ForMember(destination => destination.SaleDate, origin => origin.MapFrom(x => x.SaleDate.ToString("dd/MM/yyyy HH:mm", culture)));


        }
    }
}
