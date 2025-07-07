using HotelBookingApi.Dtos;
using HotelBookingApi.Models;

namespace HotelBookingApi.Services;

public interface IRoomService
{
    Task<List<AvailabilityDto>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, int guestSize,
        Guid? hotelId);

    Task<Room?> GetAvailableRoomByIdAsync(Guid roomId, DateTime startDate, DateTime endDate, int guestSize);

}