using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApi.Controllers;

[ApiController]
[Route("/api/data")]
public class DataController(IDataService dataService) : ControllerBase
{
    /// <summary>
    /// Seeds the database with test data.
    /// </summary>
    /// <returns>NoContent</returns>
    [HttpPost("seed")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SeedDataAsync()
    {
        await dataService.SeedDataAsync();
        return NoContent();
    }

    /// <summary>
    /// Resets the database.
    /// </summary>
    /// <returns>NoContent</returns>
    [HttpPost("reset")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResetDataAsync()
    {
        await dataService.ResetDataAsync();
        return NoContent();
    }
}