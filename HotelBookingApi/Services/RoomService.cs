using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services;

public class RoomService(
    IRoomRepository roomRepository,
    IMapper mapper) : IRoomService
{
    public async Task<List<AvailabilityDto>> GetAvailableRoomsAsync(DateTime startDate, DateTime endDate, int guestSize, Guid? hotelId)
    {
        var availableRooms = await roomRepository.GetAvailabilityAsync(startDate, endDate, guestSize, hotelId);
        return availableRooms.Select(mapper.Map<AvailabilityDto>).ToList();
    }

    public async Task<Room?> GetAvailableRoomByIdAsync(Guid roomId, DateTime startDate, DateTime endDate, int guestSize)
    {
        return await roomRepository.GetAvailableRoomAsync(roomId, startDate, endDate, guestSize);
    }
}
