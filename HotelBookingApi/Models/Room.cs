using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelBookingApi.Enums;

namespace HotelBookingApi.Models;

[Table("Rooms")]
public class Room
{
    public Room()
    {
    }

    public Room(RoomType roomType)
    {
        Type = roomType;
        Capacity = roomType switch
        {
            RoomType.Single => 1,
            RoomType.Double => 2,
            RoomType.Deluxe => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(roomType), "Invalid room type.")
        };
    }

    [Key]
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    [ForeignKey("Hotel")]
    public int HotelId { get; set; }

    public int Capacity { get; private set; }

    public RoomType Type { get; private set; }

    public Hotel Hotel { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}