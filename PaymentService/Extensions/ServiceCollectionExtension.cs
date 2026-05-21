using PaymentService.BackgroundServices;
using PaymentService.Configuration;
using PaymentService.Factories;
using PaymentService.Interfaces;

namespace PaymentService.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddSubscription<TEvent>(this IServiceCollection services, string subscriptionId)
    {
        services.AddHostedService(provider =>
        {
            var client = provider.GetRequiredService<IAirlineClient>();
            return new BackgroundSubscriptionService<TEvent>(provider, client, subscriptionId);
        });
        return services;
    }
    
    public static IServiceCollection AddRabbitMqMessageClient(this IServiceCollection services, ClientOptions options)
    {
        IAirlineClient messageClient = RabbitMqFactory.CreateMessageClient(options);
        services.AddSingleton(messageClient);
        return services;
    }
}