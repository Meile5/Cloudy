using System.Reflection;
using AirlinesBookingSystem.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Orchestrator.Database;
using Orchestrator.Database.Repositories;
using Orchestrator.Extensions;
using Orchestrator.Handlers;
using Orchestrator.Interfaces.Services;
using Orchestrator.Services;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddHostedService<Worker>();
builder.Services.AddControllers();

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<SagaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISagaRepository, SagaRepository>();
builder.Services.AddScoped<ISagaService, SagaService>();

//swagger setup (part 1)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


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
    var interfaceList = handler.GetInterfaces();
    foreach (var inter in interfaceList)
    {
        if (inter.IsGenericType && inter.GetGenericTypeDefinition() == handlerType)
        {
            builder.Services.AddScoped(inter, handler);
        }
    }
    
}

//inject rabbitmq client
var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);

//subscriptions
builder.Services.AddSubscription<BookingSuccessEvent>("booking-success-" + Guid.NewGuid());
builder.Services.AddSubscription<BookingFailEvent>("booking-failed-" + Guid.NewGuid());
builder.Services.AddSubscription<BookingStartedEvent>("booking-started-" + Guid.NewGuid());

builder.Services.AddSubscription<PaymentSuccessEvent>("payment-success-" + Guid.NewGuid());
builder.Services.AddSubscription<PaymentFailEvent>("payment-failed-" + Guid.NewGuid());
builder.Services.AddSubscription<PaymentFinalizedFailEvent>("payment-finalized-failed-" + Guid.NewGuid());
//builder.Services.AddSubscription<StartPaymentEvent>("start-payment-" + Guid.NewGuid());


var host = builder.Build();

host.MapControllers();

//swagger setup (part 2)
if (host.Environment.IsDevelopment())
{
    host.UseSwagger();
    host.UseSwaggerUI();
}

//feel free to change, this is just so it isn't the same as booking service
host.Run("http://localhost:4000");