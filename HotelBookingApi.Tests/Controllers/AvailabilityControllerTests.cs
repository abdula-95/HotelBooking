using HotelBookingApi.Controllers;
using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBookingApi.Tests.Controllers;

public class AvailabilityControllerTests
{
    private readonly AvailabilityController _controller;
    private readonly Mock<IRoomService> _roomServiceMock;

    public AvailabilityControllerTests()
    {
        _roomServiceMock = new Mock<IRoomService>();
        _controller = new AvailabilityController(_roomServiceMock.Object);
    }

    [Fact]
    public async Task GetAvailabilityAsync_ReturnsBadRequest_WhenStartDateGreaterThanEndDate()
    {
        // Act
        var result = await _controller.GetAvailabilityAsync(
            DateTime.UtcNow.AddDays(3),
            DateTime.UtcNow,
            1,
            Guid.NewGuid());

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("StartDate must be before EndDate.", response.Value);

        _roomServiceMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetAvailabilityAsync_ReturnsBadRequest_WhenGuestSizeIsLessThanZero(int guestSize)
    {
        // Act
        var result = await _controller.GetAvailabilityAsync(
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(3),
            guestSize,
            Guid.NewGuid());

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("GuestSize must be greater than zero.", response.Value);

        _roomServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetAvailabilityAsync_ReturnsSuccessfulResponse()
    {
        // Arrange
        var startDate = DateTime.UtcNow;
        var endDate = DateTime.UtcNow.AddDays(3);
        var roomId = Guid.NewGuid();

        _roomServiceMock.Setup(service =>
                service.GetAvailableRoomsAsync(
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid?>()
                ))
            .ReturnsAsync(new List<AvailabilityDto>());

        // Act
        var result = await _controller.GetAvailabilityAsync(
            startDate,
            endDate,
            1,
            roomId);

        // Assert
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(response);
        Assert.IsType<List<AvailabilityDto>>(response.Value);

        _roomServiceMock.Verify(service =>
                service.GetAvailableRoomsAsync(startDate, endDate, 1, roomId),
            Times.Once);
    }
}