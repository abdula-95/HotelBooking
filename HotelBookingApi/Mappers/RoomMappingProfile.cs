using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Models;

namespace HotelBookingApi.Mappers;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        CreateMap<Room, AvailabilityDto>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Uuid))
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uuid))
            .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}