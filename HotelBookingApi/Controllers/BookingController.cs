using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    /// <summary>
    /// Retrieve a booking by the given booking reference.
    /// </summary>
    /// <param name="reference">The booking reference.</param>
    /// <returns>Booking details of a booking.</returns>
    [HttpGet("{reference}")]
    [ProducesResponseType<BookingDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookingByReferenceAsync(string reference)
    {
        if (string.IsNullOrEmpty(reference))
            return BadRequest("Reference is required.");

        return Ok(await bookingService.GetByReferenceAsync(reference));
    }

    /// <summary>
    /// Creates a booking for a given start and end date and for a given number of people.
    /// </summary>
    /// <param name="createBookingRequestDto">The create booking request params.</param>
    /// <returns>Booking details of the newly created booking.</returns>
    [HttpPost("")]
    [ProducesResponseType<BookingDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestDto createBookingRequestDto)
    {
        if (createBookingRequestDto.StartDate >= createBookingRequestDto.EndDate)
        {
            return BadRequest("StartDate must be before EndDate.");
        }

        if (createBookingRequestDto.GuestSize <= 0)
        {
            return BadRequest("GuestSize must be greater than zero.");
        }

        return CreatedAtAction(
            nameof(CreateBooking),
            await bookingService.CreateAsync(createBookingRequestDto));
    }
}