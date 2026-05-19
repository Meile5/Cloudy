using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PaymentService.Database;
using PaymentService.Extensions;
using PaymentService.Handlers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionString"];

builder.Services.AddDbContext<PaymentContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

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
    var interfaceType = handler.GetInterfaces()
        .First(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == handlerType);

    builder.Services.AddScoped(interfaceType, handler);
}

//rabbitmq setup & subscribing 
var options = builder.Services.MessageClientOptions(builder.Configuration);
builder.Services.AddRabbitMqMessageClient(options);

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
