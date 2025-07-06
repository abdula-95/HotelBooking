using HotelBookingApi.Context;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Repositories;

public class HotelRepository(DatabaseContext databaseContext) : IHotelRepository
{

    public async Task<Hotel?> GetByNameAsync(string name)
    {
        return await databaseContext.Hotels
            .SingleOrDefaultAsync(hotel => hotel.Name == name);
    }

    public async Task<Hotel?> GetByIdAsync(Guid id)
    {
        return await databaseContext.Hotels
            .SingleOrDefaultAsync(hotel => hotel.Uuid == id);
    }

    public async Task<List<Hotel>> CreateAsync(List<Hotel> hotels)
    {
        try
        {
            await databaseContext.Hotels.AddRangeAsync(hotels);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Hotel creation failed", ex);
        }
        return hotels;
    }

    public async Task DeleteAsync()
    {
        try
        {
            databaseContext.Hotels.RemoveRange(databaseContext.Hotels);
            await databaseContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Hotel deletion failed", ex);
        }
    }
}