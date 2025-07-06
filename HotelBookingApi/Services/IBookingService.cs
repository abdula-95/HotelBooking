using HotelBookingApi.Dtos;

namespace HotelBookingApi.Services;

public interface IBookingService
{
    Task<BookingDto> GetByReferenceAsync(string reference);
    
    Task<BookingDto> CreateAsync(CreateBookingRequestDto createBookingRequestDto);
}