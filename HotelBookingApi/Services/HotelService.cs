using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Exceptions;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services;

public class HotelService(
    IHotelRepository hotelRepository,
    IMapper mapper) : IHotelService
{
    public async Task<HotelDto> GetByNameAsync(string name)
    {
        var hotel = await hotelRepository.GetByNameAsync(name);

        if (hotel is null)
            throw new NotFoundException($"Hotel with name {name} was not found.");

        return mapper.Map<HotelDto>(hotel);
    }
}