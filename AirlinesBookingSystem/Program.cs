using System.Reflection;
using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Repositories;
using AirlinesBookingSystem.Database.MongoDb.Services;
using AirlinesBookingSystem.Extensions;
using AirlinesBookingSystem.Handlers;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Repositories;
using AirlinesBookingSystem.Services;
using AirlinesFlightsystem.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MongoDB.Driver;
using Shared.Events;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddHostedService<Worker>();

builder.Services.AddControllers();


//db context 
var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<BookingContext>(options =>
    options.UseSqlServer(connectionString));


//swagger setup (part 1)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


//dependency injection
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();

builder.Services.AddScoped<IMongoSeatRepository, MongoSeatRepository>();
builder.Services.AddScoped<IMongoFlightRepository, MongoFlightRepository>();
builder.Services.AddScoped<IMongoSeatService, MongoSeatService>();
builder.Services.AddScoped<IMongoFlightService, MongoFlightService>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ISeatService, SeatService>();


//mongo db setup
builder.Services.AddSingleton<IMongoClient>(
    new MongoClient(builder.Configuration["MongoDB:ConnectionString"]));

builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>()
        .GetDatabase(builder.Configuration["MongoDB:Database"]));

// Auto-register handlers via reflection
// ------------------------------------------------------------
var handlerType = typeof(IEventHandler<>);

var handlers = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => !t.IsAbstract &&
                !t.IsInterface &&
                t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == handlerType))
    .ToList();

foreach (var handler in handlers)
{
    var interfaceType = handler.GetInterfaces()
        .First(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == handlerType);

    builder.Services.AddScoped(interfaceType, handler);
}

//redis setup
builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration["Redis:ConnectionString"] ?? "localhost:6379"
    )
);
builder.Services.AddScoped<ISeatLockService, SeatLockService>();

//rabbitmq setup & subscribing 
var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);
builder.Services.AddSubscription<PaymentSuccessStartBookingEvent>("new-subscriber-" + Guid.NewGuid());
builder.Services.AddSubscription<MongoAddSeatCommand>("mongo-seat-" + Guid.NewGuid());
builder.Services.AddSubscription<MongoRemoveSeatCommand>("remove-set-" + Guid.NewGuid());
builder.Services.AddSubscription<MongoAddFlightCommand>("mongo-add-flight-" + Guid.NewGuid());




var host = builder.Build();

host.MapControllers();


//swagger setup (part 2)
/*if (host.Environment.IsDevelopment())
{*/
    host.UseSwagger();
    host.UseSwaggerUI();
//}



host.Run();