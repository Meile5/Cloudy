using System.Reflection;
using Microsoft.OpenApi;
using MongoDB.Driver;
using MongoReadModel.Extensions;
using MongoReadModel.Handlers;
using MongoReadModel.MongoDb.Interfaces;
using MongoReadModel.MongoDb.Repositories;
using MongoReadModel.MongoDb.Services;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);


//swagger setup (part 1)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

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

//dependency injection
builder.Services.AddScoped<IMongoFlightRepository, MongoFlightRepository>();
builder.Services.AddScoped<IMongoFlightService, MongoFlightService>();

//add controllers
builder.Services.AddControllers();

//rabbitmq setup & subscribing 
var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);
builder.Services.AddSubscription<MongoAddSeatCommand>("mongo-seat-" + Guid.NewGuid());
builder.Services.AddSubscription<MongoRemoveSeatCommand>("remove-set-" + Guid.NewGuid());
builder.Services.AddSubscription<MongoAddFlightCommand>("mongo-add-flight-" + Guid.NewGuid());

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();