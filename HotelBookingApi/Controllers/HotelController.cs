using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController(IHotelService hotelService) : ControllerBase
{
    /// <summary>
    /// Retrieve a hotel by the given name.
    /// </summary>
    /// <param name="name">The name of the hotel</param>
    /// <returns>Hotels details of the hotel.</returns>
    [HttpGet("search")]
    [ProducesResponseType<HotelDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name is required.");
        }

        return Ok(await hotelService.GetByNameAsync(name));
    }
}