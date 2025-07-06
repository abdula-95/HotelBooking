using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByNameAsync(string name);

    Task<Hotel?> GetByIdAsync(Guid id);

    Task<List<Hotel>> CreateAsync(List<Hotel> hotels);

    Task DeleteAsync();
}