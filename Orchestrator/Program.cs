using System.Reflection;
using AirlinesBookingSystem.Extensions;
using AirlinesBookingSystem.Handlers;
using AirlinesBookingSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Orchestrator;
using Orchestrator.Database;
using Orchestrator.Database.Repositories;
using Orchestrator.Interfaces.Services;
using Orchestrator.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<SagaContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISagaRepository, SagaRepository>();
builder.Services.AddScoped<ISagaService, SagaService>();


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

var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);
var host = builder.Build();
host.Run();