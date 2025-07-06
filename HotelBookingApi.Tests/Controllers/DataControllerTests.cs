using HotelBookingApi.Controllers;
using HotelBookingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HotelBookingApi.Tests.Controllers;

public class DataControllerTests
{
    private readonly DataController _controller;
    private readonly Mock<IDataService> _dataServiceMock;

    public DataControllerTests()
    {
        _dataServiceMock = new Mock<IDataService>();
        _controller = new DataController(_dataServiceMock.Object);
    }

    [Fact]
    public async Task SeedDataAsync_ReturnsNoContentResponseOnSuccess()
    {
        // Act
        var result = await _controller.SeedDataAsync();

        // Assert
        Assert.IsType<NoContentResult>(result);

        _dataServiceMock.Verify(x => x.SeedDataAsync(), Times.Once);
    }

    [Fact]
    public async Task ResetDataAsync_ReturnsNoContentResponseOnSuccess()
    {
        // Act
        var result = await _controller.ResetDataAsync();

        // Assert
        Assert.IsType<NoContentResult>(result);

        _dataServiceMock.Verify(x => x.ResetDataAsync(), Times.Once);
    }
}