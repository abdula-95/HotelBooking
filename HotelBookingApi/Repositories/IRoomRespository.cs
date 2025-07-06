using HotelBookingApi.Models;

namespace HotelBookingApi.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAvailabilityAsync(DateTime startDate, DateTime endDate, int guestCount, Guid? hotelId);

    Task<Room?> GetAvailableRoomAsync(Guid roomId, DateTime startDate, DateTime endDate, int guestSize);

    Task DeleteAsync();
}