using AirlinesBookingSystem.Handlers;

namespace AirlinesBookingSystem.Interfaces;

public interface IAirlineClient
{
    public Task Subscribe<T>(string subscriptionId, AirlineHandler<T>? handler,
        CancellationToken token = default);

    public Task Publish<T>(T message, CancellationToken token = default);
    public Task Unsubscribe(string subscriptionId);
}