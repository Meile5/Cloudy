using System.Reflection;
using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Extensions;
using AirlinesBookingSystem.Handlers;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Repositories;
using AirlinesBookingSystem.Services;
using AirlinesFlightsystem.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


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

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<ISeatService, SeatService>();


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

//rabbitmq setup & subscribing 
var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);
builder.Services.AddSubscription<PaymentSuccessStartBookingEvent>("new-subscriber");



var host = builder.Build();

host.MapControllers();


//swagger setup (part 2)
if (host.Environment.IsDevelopment())
{
    //host.MapOpenApi();
    host.UseSwagger();
    //host.UseSwaggerUi(options => { options.DocumentPath = "/openapi/v1.json"; });
    //host.UseSwaggerUi(options => { options.DocumentPath = "/swagger/v1/swagger.json"; });
    host.UseSwaggerUI();
}



host.Run();