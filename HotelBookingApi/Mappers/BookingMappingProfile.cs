using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Models;

namespace HotelBookingApi.Mappers;

public class BookingMappingProfile : Profile
{
    public BookingMappingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid))
            .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.Room.Hotel));
    }
}