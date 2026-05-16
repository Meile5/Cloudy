using AirlinesBookingSystem.Configuration;
using Orchestrator.Factories;
using Orchestrator.Interfaces;

namespace Orchestrator.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddRabbitMqMessageClient(this IServiceCollection services, ClientOptions options)
    {
        IAirlineClient messageClient = RabbitMqFactory.CreateMessageClient(options);
        services.AddSingleton(messageClient);
        return services;
    }
}