using System.ComponentModel.DataAnnotations;

namespace HotelBookingApi.Dtos;

public class CreateBookingRequestDto
{
    [Required]
    public Guid RoomId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int GuestSize { get; set; }
}