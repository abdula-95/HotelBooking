using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers;

[ApiController]
[Route("api/availability")]
public class AvailabilityController(IRoomService roomService) : ControllerBase
{
    /// <summary>
    /// Retrieve room availability between two dates for a given number of people.
    /// </summary>
    /// <param name="startDate">The start date of the search.</param>
    /// <param name="endDate">The end date of the search.</param>
    /// <param name="guestSize">The number of guests.</param>
    /// <param name="hotelId">(Optional) The ID of the hotel to book against.</param>
    /// <returns>A list of available rooms.</returns>
    [HttpGet("")]
    [ProducesResponseType<List<AvailabilityDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAvailabilityAsync(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int guestSize,
        [FromQuery] Guid? hotelId)
    {
        if (startDate >= endDate)
        {
            return BadRequest("StartDate must be before EndDate.");
        }

        if (guestSize <= 0)
        {
            return BadRequest("GuestSize must be greater than zero.");
        }

        return Ok(await roomService.GetAvailableRoomsAsync(startDate, endDate, guestSize, hotelId));
    }
}