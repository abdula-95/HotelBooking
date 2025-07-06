using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories;

public interface IBookingRepository
{
    Task<Booking?> GetByReferenceAsync(string reference);

    Task<Booking?> GetByIdAsync(Guid id);

    Task<Booking> CreateAsync(Booking booking);

    Task<List<Booking>> CreateAsync(List<Booking> bookings);

    Task DeleteAsync();
}