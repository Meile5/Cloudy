using AirlinesBookingSystem.Interfaces;
using PaymentService.Handlers;
using PaymentService.Interfaces;

namespace PaymentService.BackgroundServices;


public class BackgroundSubscriptionService<TEvent> : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly IAirlineClient _client;
    private readonly string _subscriptionId;

    public BackgroundSubscriptionService(
        IServiceProvider services, IAirlineClient client, string subscriptionId)
    {
        _services = services;
        _client = client;
        _subscriptionId = subscriptionId;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await _client.Subscribe<TEvent>(
            _subscriptionId,
            new AirlineHandler<TEvent>(async message =>
            {
                using var scope = _services.CreateScope();
                var handler = scope.ServiceProvider
                    .GetRequiredService<IEventHandler<TEvent>>();
                await handler.HandleAsync(message, ct);
            }),
            ct
        );

        await Task.Delay(Timeout.Infinite, ct);
    }
}