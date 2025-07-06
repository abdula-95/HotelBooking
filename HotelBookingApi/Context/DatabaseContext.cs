using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hotel>()
            .Property(hotel => hotel.Uuid)
            .HasDefaultValueSql("newid()");

        modelBuilder.Entity<Room>()
            .Property(room => room.Uuid)
            .HasDefaultValueSql("newid()");

        modelBuilder.Entity<Booking>()
            .Property(booking => booking.Uuid)
            .HasDefaultValueSql("newid()");

        modelBuilder.Entity<Booking>()
            .Property(b => b.StartDate)
            .HasColumnType("date");

        modelBuilder.Entity<Booking>()
            .Property(b => b.EndDate)
            .HasColumnType("date");
    }
}