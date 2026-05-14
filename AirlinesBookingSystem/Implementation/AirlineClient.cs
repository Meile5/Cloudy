using AirlinesBookingSystem.Interfaces;

namespace AirlinesBookingSystem.Implementation;


public class AirlineClient(IEventsAdapter adapter) : IAirlineClient
{
    public async Task Subscribe<T>(string subscriptionId, EventHandler<T>? handler = null, CancellationToken token = default)
    {
        await adapter.Subscribe(subscriptionId, handler, token);
    }

    public async Task Publish<T>(T message, CancellationToken token = default)
    {
        await adapter.Publish(message, token);
    }
    public async Task Unsubscribe(string subscriptionId)
    {
        await adapter.Unsubscribe(subscriptionId);
    }
}