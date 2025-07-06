using HotelBookingApi.Context;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories;

public class BookingRepository(DatabaseContext databaseContext) : IBookingRepository
{
    public async Task<Booking?> GetByReferenceAsync(string reference)
    {
        return await databaseContext.Bookings
            .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
            .SingleOrDefaultAsync(booking => booking.Reference == reference);
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await databaseContext.Bookings
            .Include(booking => booking.Room)
                .ThenInclude(room => room.Hotel)
            .SingleOrDefaultAsync(booking => booking.Uuid == id);
    }

    public async Task<Booking?> GetByReference(string reference)
    {
        return await databaseContext.Bookings
            .Include(booking => booking.Room)
            .ThenInclude(room => room.Hotel)
            .SingleOrDefaultAsync(booking => booking.Reference == reference);
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        try
        {
            await databaseContext.Bookings.AddAsync(booking);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Booking creation failed", ex);
        }

        return booking;
    }

    public async Task<List<Booking>> CreateAsync(List<Booking> bookings)
    {
        try
        {
            await databaseContext.Bookings.AddRangeAsync(bookings);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Booking creation failed", ex);
        }
        return bookings;
    }

    public async Task DeleteAsync()
    {
        try
        {
            databaseContext.Bookings.RemoveRange(databaseContext.Bookings);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Booking deletion failed", ex);
        }
    }
}