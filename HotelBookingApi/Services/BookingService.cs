using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Exceptions;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;

namespace HotelBookingApi.Services;

public class BookingService(
    IBookingRepository bookingRepository,
    IRoomService roomService,
    IMapper mapper) : IBookingService
{
    public async Task<BookingDto> GetByReferenceAsync(string reference)
    {
        var booking = await bookingRepository.GetByReferenceAsync(reference);

        if (booking is null)
            throw new NotFoundException($"Booking with reference {reference} was not found");

        return mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CreateAsync(CreateBookingRequestDto createBookingRequestDto)
    {
        var availableRoom = await roomService.GetAvailableRoomByIdAsync(
            createBookingRequestDto.RoomId,
            createBookingRequestDto.StartDate,
            createBookingRequestDto.EndDate,
            createBookingRequestDto.GuestSize);

        if (availableRoom is null)
        {
            throw new ConflictException($"No availability was found with room with Id {createBookingRequestDto.RoomId}");
        }

        var booking = new Booking
        {
            Reference = await GenerateBookingReference(),
            RoomId = availableRoom.Id,
            StartDate = createBookingRequestDto.StartDate,
            EndDate = createBookingRequestDto.EndDate,
            GuestSize = createBookingRequestDto.GuestSize
        };

        await bookingRepository.CreateAsync(booking);
        return mapper.Map<BookingDto>(await bookingRepository.GetByIdAsync(booking.Uuid));
    }

    private async Task<string> GenerateBookingReference()
    {
        var isValid = false;
        var bookingReference = "";

        while (!isValid)
        {
            bookingReference = Guid.NewGuid().ToString("N")[..8].ToUpper();
            if (await bookingRepository.GetByReferenceAsync(bookingReference) is null)
            {
                isValid = true;
            }
        }

        return bookingReference;
    }

}