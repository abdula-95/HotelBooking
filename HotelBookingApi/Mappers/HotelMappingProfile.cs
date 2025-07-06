using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Models;

namespace HotelBookingApi.Mappers;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        CreateMap<Hotel, HotelDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid));
    }
}