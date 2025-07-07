using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Enums;
using HotelBookingApi.Mappers;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Moq;

namespace HotelBookingApi.Tests.Services;

public class RoomServiceTests
{
    private readonly IRoomService _service;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;

    public RoomServiceTests()
    {
        _roomRepositoryMock = new Mock<IRoomRepository>();

        _service = new RoomService(_roomRepositoryMock.Object,
            new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<RoomMappingProfile>();
            })));
    }

    [Fact]
    public async Task GetAvailableRoomsAsync_ReturnsAvailableRooms()
    {
        // Arrange
        _roomRepositoryMock.Setup(repository => repository.GetAvailabilityAsync(
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<int>(),
            It.IsAny<Guid>()
        )).ReturnsAsync(new List<Room> { new Room(RoomType.Single) });

        // Act
        var result = await _service.GetAvailableRoomsAsync(DateTime.UtcNow, DateTime.UtcNow.AddDays(2), 1, Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.IsType<List<AvailabilityDto>>(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAvailableRoomByIdAsync_ReturnsAvailableRoom()
    {
        // Arrange
        var roomId = Guid.NewGuid();

        _roomRepositoryMock.Setup(repository => repository.GetAvailableRoomAsync(
            It.IsAny<Guid>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>(),
            It.IsAny<int>()
        )).ReturnsAsync(new Room
        {
            Id = 1,
            Uuid = roomId
        });

        // Act
        var result = await _service.GetAvailableRoomByIdAsync(roomId, DateTime.UtcNow, DateTime.UtcNow.AddDays(2), 1);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Room>(result);
        Assert.Equal(roomId, result.Uuid);
    }

}