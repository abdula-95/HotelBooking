using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Exceptions;
using HotelBookingApi.Mappers;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Moq;

namespace HotelBookingApi.Tests.Services;

public class HotelServiceTests
{
    private readonly IHotelService _service;
    private readonly Mock<IHotelRepository> _hotelRepositoryMock;

    public HotelServiceTests()
    {
        _hotelRepositoryMock = new Mock<IHotelRepository>();

        _service = new HotelService(
            _hotelRepositoryMock.Object,
            new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<HotelMappingProfile>();
            })));
    }

    [Fact]
    public async Task GetByNameAsync_ReturnsNotFoundException_WhenHotelNotFound()
    {
        // Arrange
        _hotelRepositoryMock.Setup(repository =>
            repository.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Hotel);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _service.GetByNameAsync("Hotel A"));

        // Assert
        Assert.Equal("Hotel with name Hotel A was not found.", exception.Message);
    }

    [Fact]
    public async Task GetByNameAsync_ReturnsHotelDto_OnSuccess()
    {
        // Arrange
        const string hotelName = "Hotel A";

        _hotelRepositoryMock.Setup(repository =>
                repository.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new Hotel
            {
                Id = 1,
                Name = hotelName,
                Uuid = Guid.NewGuid()
            });

        // Act
        var result = await _service.GetByNameAsync(hotelName);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<HotelDto>(result);
        Assert.Equal(hotelName, result.Name);
    }
}