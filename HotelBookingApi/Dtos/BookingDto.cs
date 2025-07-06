namespace HotelBookingApi.Dtos;

public class BookingDto
{
    public Guid Id { get; set; }

    public string Reference { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int GuestSize { get; set; }

    public HotelDto Hotel { get; set; } = null!;

    public RoomDto Room { get; set; } = null!;
}