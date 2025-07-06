using HotelBookingApi.Controllers;
using HotelBookingApi.Dtos;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBookingApi.Tests.Controllers;

public class HotelControllerTests
{
    private readonly HotelController _controller;
    private readonly Mock<IHotelService> _hotelServiceMock;

    public HotelControllerTests()
    {
        _hotelServiceMock = new Mock<IHotelService>();
        _controller = new HotelController(_hotelServiceMock.Object);
    }

    [Fact]
    public async Task Search_ReturnsBadRequest_WhenNameParamIsEmpty()
    {
        // Act
        var result = await _controller.Search(string.Empty);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Name is required.", response.Value);

        _hotelServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Search_ReturnsBadRequest_WhenNameParamIsNull()
    {
        // Act
        var result = await _controller.Search(null!);

        // Assert
        var response = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Name is required.", response.Value);

        _hotelServiceMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Search_ReturnsSuccessfulResponse()
    {
        // Arrange
        _hotelServiceMock.Setup(service =>
                service.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new HotelDto());

        // Act
        var result = await _controller.Search("Hotel A");

        // Assert
        var response = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(response);
        Assert.IsType<HotelDto>(response.Value);

        _hotelServiceMock.Verify(service => service.GetByNameAsync("Hotel A"),
            Times.Once);
    }
}