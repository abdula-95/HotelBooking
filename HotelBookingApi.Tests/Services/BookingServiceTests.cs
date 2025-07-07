using AutoMapper;
using HotelBookingApi.Dtos;
using HotelBookingApi.Exceptions;
using HotelBookingApi.Mappers;
using HotelBookingApi.Models;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Moq;

namespace HotelBookingApi.Tests.Services;

public class BookingServiceTests
{
    private readonly IBookingService _service;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly Mock<IRoomService> _roomServiceMock;

    public BookingServiceTests()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _roomServiceMock = new Mock<IRoomService>();

        _service = new BookingService(
            _bookingRepositoryMock.Object,
            _roomServiceMock.Object,
            new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<BookingMappingProfile>();
                c.AddProfile<RoomMappingProfile>();
            })));
    }

    [Fact]
    public async Task GetByReferenceAsync_ReturnsNotFoundException_WhenBookingNotFound()
    {
        // Arrange
        _bookingRepositoryMock.Setup(repository =>
                repository.GetByReferenceAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Booking);

        // Act
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _service.GetByReferenceAsync("booking-ref"));

        // Assert
        Assert.Equal("Booking with reference booking-ref was not found.", exception.Message);
    }

    [Fact]
    public async Task GetByReferenceAsync_ReturnsBookingDto_WhenBookingExists()
    {
        // Arrange
        const string bookingReference = "booking-ref";

        _bookingRepositoryMock.Setup(repository =>
                repository.GetByReferenceAsync(It.IsAny<string>()))
            .ReturnsAsync(new Booking()
            {
                Id = 1,
                Reference = bookingReference,
                Uuid = Guid.NewGuid()
            });

        // Act
        var result = await _service.GetByReferenceAsync(bookingReference);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BookingDto>(result);
        Assert.Equal(bookingReference, result.Reference);
    }

    [Fact]
    public async Task CreateAsync_ReturnsConflictException_WhenRoomNotAvailable()
    {
        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            RoomId = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            GuestSize = 1
        };

        _roomServiceMock.Setup(service =>
                service.GetAvailableRoomByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>()
                ))
            .ReturnsAsync(null as Room);

        // Act
        var exception = await Assert.ThrowsAsync<ConflictException>(async () =>
            await _service.CreateAsync(createBookingRequest));

        // Assert
        Assert.Equal($"No availability was found with room with Id {createBookingRequest.RoomId}.", exception.Message);

        _roomServiceMock.Verify(service => service.GetAvailableRoomByIdAsync(
            createBookingRequest.RoomId,
            createBookingRequest.StartDate,
            createBookingRequest.EndDate,
            createBookingRequest.GuestSize));
    }

    [Fact]
    public async Task CreateAsync_CreatesAndReturnsBookingDtoForAvailableRoom()
    {
        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            RoomId = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            GuestSize = 1
        };

        var booking = new Booking
        {
            Id = 1,
            Uuid = Guid.NewGuid(),
            RoomId = 1,
            Room = new Room { Id = 1, Uuid = createBookingRequest.RoomId },
            StartDate = createBookingRequest.StartDate,
            EndDate = createBookingRequest.EndDate,
            GuestSize = createBookingRequest.GuestSize,
        };

        _roomServiceMock.Setup(service =>
                service.GetAvailableRoomByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>()
                ))
            .ReturnsAsync(new Room
            {
                Id = 1
            });

        _bookingRepositoryMock.Setup(repository => repository.CreateAsync(
            It.IsAny<Booking>())).ReturnsAsync(booking);

        _bookingRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(booking);

        // Act
        var result = await _service.CreateAsync(createBookingRequest);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BookingDto>(result);
        Assert.Equal(createBookingRequest.RoomId, result.Room.Id);
        Assert.Equal(createBookingRequest.StartDate, result.StartDate);
        Assert.Equal(createBookingRequest.EndDate, result.EndDate);
        Assert.Equal(createBookingRequest.GuestSize, result.GuestSize);

        _bookingRepositoryMock.Verify(repository => repository.CreateAsync(It.Is<Booking>(b =>
            b.RoomId == 1 &&
            b.StartDate == createBookingRequest.StartDate &&
            b.EndDate == createBookingRequest.EndDate &&
            b.GuestSize == createBookingRequest.GuestSize)),
            Times.Once);
    }

    [Fact]
    public async Task CreateAsync_CreatesBookingWithUniqueReference()
    {
        const string bookingReference = "123455678";

        // Arrange
        var createBookingRequest = new CreateBookingRequestDto
        {
            RoomId = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            GuestSize = 1
        };

        var booking = new Booking
        {
            Id = 1,
            Reference = bookingReference,
            Uuid = Guid.NewGuid(),
            RoomId = 1,
            Room = new Room { Id = 1, Uuid = createBookingRequest.RoomId },
            StartDate = createBookingRequest.StartDate,
            EndDate = createBookingRequest.EndDate,
            GuestSize = createBookingRequest.GuestSize,
        };

        _roomServiceMock.Setup(service =>
                service.GetAvailableRoomByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>()
                ))
            .ReturnsAsync(new Room
            {
                Id = 1
            });

        _bookingRepositoryMock.Setup(repository => repository.CreateAsync(
            It.IsAny<Booking>())).ReturnsAsync(booking);

        _bookingRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(booking);

        _bookingRepositoryMock.Setup(repository => repository.GetByReferenceAsync(It.IsAny<string>()))
            .ReturnsAsync(null as Booking);

        // Act
        var result = await _service.CreateAsync(createBookingRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookingReference, result.Reference);

        _bookingRepositoryMock.Verify(repository => repository.GetByReferenceAsync(It.Is<string>(c => c.Length == 8)),
            Times.Once);
    }
}