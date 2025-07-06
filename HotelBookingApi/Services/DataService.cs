using System.Transactions;
using HotelBookingApi.Helpers;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services;

public class DataService(
    IHotelRepository hotelRepository,
    IRoomRepository roomRepository,
    IBookingRepository bookingRepository) : IDataService
{
    public async Task SeedDataAsync()
    {
        var hotels = DataSeedHelper.GetHotels();

        await hotelRepository.CreateAsync(hotels);
        await bookingRepository.CreateAsync(DataSeedHelper.GetBookings(hotels.SelectMany(h => h.Rooms).ToList()));
    }

    public async Task ResetDataAsync()
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            await bookingRepository.DeleteAsync();
            await roomRepository.DeleteAsync();
            await hotelRepository.DeleteAsync();

            scope.Complete();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to reset data, please try again", ex);
        }
    }
}