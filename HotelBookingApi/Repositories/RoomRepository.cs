using HotelBookingApi.Context;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories;

public class RoomRepository(DatabaseContext databaseContext) : IRoomRepository
{
    public async Task<List<Room>> GetAvailabilityAsync(DateTime startDate, DateTime endDate, int guestSize, Guid? hotelId)
    {
        var query = databaseContext.Rooms
            .Where(room => guestSize <= room.Capacity)
            .Where(room => !room.Bookings.Any(booking =>
                startDate < booking.EndDate && endDate > booking.StartDate));

        if (hotelId.HasValue)
        {
            query = query.Where(room => room.Hotel.Uuid == hotelId.Value);
        }

        query = query.Include(room => room.Hotel)
            .OrderBy(room => room.Capacity);

        return await query.ToListAsync();
    }

    public async Task<Room?> GetAvailableRoomAsync(Guid roomId, DateTime startDate, DateTime endDate, int guestSize)
    {
        return await databaseContext.Rooms
            .FirstOrDefaultAsync(room =>
                room.Uuid == roomId &&
                guestSize <= room.Capacity &&
                !room.Bookings.Any(booking => startDate < booking.EndDate && endDate > booking.StartDate));
    }

    public async Task DeleteAsync()
    {
        try
        {
            databaseContext.Rooms.RemoveRange(databaseContext.Rooms);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Room deletion failed", ex);
        }
    }
}