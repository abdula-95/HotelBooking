using HotelBookingApi.Enums;
using HotelBookingApi.Models;

namespace HotelBookingApi.Helpers;

public static class DataSeedHelper
{
    public static List<Hotel> GetHotels()
    {
        return
        [
            new Hotel
            {
                Name = "Alpha",
                Rooms = new List<Room>
                {
                    new Room(RoomType.Single),
                    new Room(RoomType.Single),
                    new Room(RoomType.Double),
                    new Room(RoomType.Double),
                    new Room(RoomType.Deluxe),
                    new Room(RoomType.Deluxe),
                }
            },

            new Hotel
            {
                Name = "Bravo",
                Rooms = new List<Room>
                {
                    new Room(RoomType.Single),
                    new Room(RoomType.Single),
                    new Room(RoomType.Double),
                    new Room(RoomType.Double),
                    new Room(RoomType.Deluxe),
                    new Room(RoomType.Deluxe),
                }
            },

            new Hotel
            {
                Name = "Charlie",
                Rooms = new List<Room>
                {
                    new Room(RoomType.Single),
                    new Room(RoomType.Single),
                    new Room(RoomType.Double),
                    new Room(RoomType.Double),
                    new Room(RoomType.Deluxe),
                    new Room(RoomType.Deluxe),
                }
            },

            new Hotel
            {
                Name = "Delta",
                Rooms = new List<Room>
                {
                    new Room(RoomType.Single),
                    new Room(RoomType.Single),
                    new Room(RoomType.Double),
                    new Room(RoomType.Double),
                    new Room(RoomType.Deluxe),
                    new Room(RoomType.Deluxe),
                }
            }
        ];
    }

    public static List<Booking> GetBookings(List<Room> rooms)
    {
        return
        [
            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7),
                GuestSize = 1,
                RoomId = rooms.First(r => r is { Type: RoomType.Single, Hotel.Name: "Alpha" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(2),
                GuestSize = 2,
                RoomId = rooms.First(r => r is { Type: RoomType.Double, Hotel.Name: "Alpha" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(5),
                GuestSize = 3,
                RoomId = rooms.First(r => r is { Type: RoomType.Deluxe, Hotel.Name: "Bravo" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(3),
                GuestSize = 1,
                RoomId = rooms.First(r => r is { Type: RoomType.Single, Hotel.Name: "Charlie" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow.AddDays(4),
                EndDate = DateTime.UtcNow.AddDays(8),
                GuestSize = 4,
                RoomId = rooms.First(r => r is { Type: RoomType.Deluxe, Hotel.Name: "Charlie" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(4),
                GuestSize = 2,
                RoomId = rooms.First(r => r is { Type: RoomType.Double, Hotel.Name: "Delta" }).Id
            },

            new Booking
            {
                Reference = Guid.NewGuid().ToString("N")[..8].ToUpper(),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(7),
                GuestSize = 1,
                RoomId = rooms.First(r => r is { Type: RoomType.Single, Hotel.Name: "Delta" }).Id
            }
        ];
    }
}