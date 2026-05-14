namespace AirlinesBookingSystem.Interfaces;


public interface IEventsAdapter
{
    public Task Subscribe<T>(string subscriptionId, EventHandler<T>? handler,
        CancellationToken token = default);

    public Task Publish<T>(T message, CancellationToken token = default);
    public Task Unsubscribe(string subscriptionId);
}