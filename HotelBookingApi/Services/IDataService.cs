namespace HotelBookingApi.Services;

public interface IDataService
{
    Task SeedDataAsync();

    Task ResetDataAsync();
}