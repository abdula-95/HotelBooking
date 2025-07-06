namespace HotelBookingApi.Dtos;

public class AvailabilityDto
{
    public Guid RoomId { set; get; }

    public HotelDto Hotel { get; set; } = new();

    public string RoomType { get; set; } = null!;

    public int Capacity { get; set; }
}