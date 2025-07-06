namespace HotelBookingApi.Dtos;

public class RoomDto
{
    public Guid RoomId { set; get; }

    public string RoomType { get; set; } = null!;

    public int Capacity { get; set; }
}