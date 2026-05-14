using AirlinesBookingSystem.BackgroundServices;
using AirlinesBookingSystem.Configuration;
using AirlinesBookingSystem.Factories;
using AirlinesBookingSystem.Interfaces;

namespace AirlinesBookingSystem.Extensions;

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
}