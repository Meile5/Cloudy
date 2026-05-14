using AirlinesBookingSystem.Configuration;
using AirlinesBookingSystem.Factories;
using AirlinesBookingSystem.Interfaces;

namespace AirlinesBookingSystem.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddRabbitMqMessageClient(this IServiceCollection services, ClientOptions options)
    {
        IAirlineClient messageClient = RabbitMqFactory.CreateMessageClient(options);
        services.AddSingleton(messageClient);
        return services;
    }
}