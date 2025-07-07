using HotelBookingApi.Context;
using HotelBookingApi.Mappers;
using HotelBookingApi.Middleware;
using HotelBookingApi.Repositories;
using HotelBookingApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register Services
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IDataService, DataService>();

// Register Repositories
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddAutoMapper(typeof(HotelMappingProfile));
builder.Services.AddAutoMapper(typeof(BookingMappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hotel Booking API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API V1");
    options.RoutePrefix = "swagger";
});

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
