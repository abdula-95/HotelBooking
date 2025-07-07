using HotelBookingApi.Controllers;
using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBookingApi.Tests.Controllers;

public class BookingControllerTests
{
    private readonly BookingController _controller;
    private readonly Mock<IBookingService> _bookingServiceMock;

    public BookingControllerTests()
    {
        _bookingServiceMock = new Mock<IBookingService>();
        _controller = new BookingController(_bookingServiceMock.Object);
    }

    [Fact]
    public async Task GetBookingByReferenceAsync_ReturnsBadRequest_WhenReferenceParamIsEmpty()
    {
        // Act
        var result = await _controller.GetBookingByReferenceAsync(string.Empty);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Reference is required.", response.Value);

        _bookingServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetBookingByReferenceAsync_ReturnsBadRequest_WhenNameParamIsNull()
    {
        // Act
        var result = await _controller.GetBookingByReferenceAsync(null!);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Reference is required.", response.Value);

        _bookingServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task GetBookingByReferenceAsync_ReturnsSuccessfulResponse()
    {
        // Arrange
        _bookingServiceMock.Setup(service =>
                service.GetByReferenceAsync(It.IsAny<string>()))
            .ReturnsAsync(new BookingDto());

        // Act
        var result = await _controller.GetBookingByReferenceAsync("Reference");

        // Assert
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(response);
        Assert.IsType<BookingDto>(response.Value);

        _bookingServiceMock.Verify(service => service.GetByReferenceAsync("Reference"),
            Times.Once);
    }

    [Fact]
    public async Task CreateBooking_ReturnsBadRequest_WhenStartDateGreaterThanEndDate()
    {
        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            StartDate = DateTime.UtcNow.AddDays(3),
            EndDate = DateTime.UtcNow,
            GuestSize = 1,
            RoomId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.CreateBooking(createBookingRequest);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("StartDate must be before EndDate.", response.Value);

        _bookingServiceMock.VerifyNoOtherCalls();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task CreateBooking_ReturnsBadRequest_WhenGuestSizeIsLessThanZero(int guestSize)
    {
        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            GuestSize = guestSize,
            RoomId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.CreateBooking(createBookingRequest);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("GuestSize must be greater than zero.", response.Value);

        _bookingServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task CreateBooking_ReturnsSuccessfulResponse()
    {
        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(3),
            GuestSize = 1,
            RoomId = Guid.NewGuid()
        };

        _bookingServiceMock.Setup(service =>
                service.CreateAsync(
            It.IsAny<CreateBookingRequestDto>()
                ))
            .ReturnsAsync(new BookingDto());

        // Act
        var result = await _controller.CreateBooking(createBookingRequest);

        // Assert
        var response = Assert.IsType<CreatedAtActionResult>(result);
        Assert.NotNull(response);
        Assert.IsType<BookingDto>(response.Value);

        _bookingServiceMock.Verify(service =>
                service.CreateAsync(It.Is<CreateBookingRequestDto>(dto =>
                    dto.StartDate == createBookingRequest.StartDate &&
                    dto.EndDate == createBookingRequest.EndDate &&
                    dto.GuestSize == createBookingRequest.GuestSize &&
                    dto.RoomId == createBookingRequest.RoomId)),
            Times.Once);
    }
}