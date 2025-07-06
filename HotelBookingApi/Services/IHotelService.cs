using HotelBookingApi.Dtos;

namespace HotelBookingApi.Services;

public interface IHotelService
{
    Task<HotelDto> GetByNameAsync(string name);
}