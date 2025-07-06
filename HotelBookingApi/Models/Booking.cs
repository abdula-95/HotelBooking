using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.Models;

[Table("Bookings")]
public class Booking
{
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    [StringLength(8)]
    public string Reference { get; set; } = null!;

    [ForeignKey("Room")]
    public int RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int GuestSize { get; set; }
}