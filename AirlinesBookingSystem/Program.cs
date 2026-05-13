using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AirlinesBookingSystem;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Repositories;
using AirlinesBookingSystem.Services;
using AirlinesFlightsystem.Repositories;


var builder = WebApplication.CreateBuilder();
//builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();

//dependency injection
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ISeatService, SeatService>();

var host = builder.Build();

host.MapControllers();

host.Run();