using System.Collections.Concurrent;
using AirlinesBookingSystem.Handlers;
using AirlinesBookingSystem.Interfaces;
using EasyNetQ.Internals;

namespace AirlinesBookingSystem.Adapters;
using EasyNetQ;


public class AirlinesAdapter(IBus _bus) : IEventsAdapter
{
    private ConcurrentDictionary<string, SubscriptionResult> _subscriptions = new ConcurrentDictionary<string, SubscriptionResult>();
    
    public async Task Subscribe<T>(string subscriptionId, AirlineHandler<T>? handler, CancellationToken token = default)
    {
        if (handler == null)
        {
            handler = new AirlineHandler<T>(DefaultHandleTextMessage);
        }
        
        var result = await _bus.PubSub.SubscribeAsync<T>(subscriptionId, handler.Handler, token);
        _subscriptions.TryAdd(subscriptionId, result);
    }

    public async Task Publish<T>(T message, CancellationToken token = default)
    {
        Console.WriteLine($"Publishing message: {message}");
        await _bus.PubSub.PublishAsync(message, token);
        Console.WriteLine("Message sent!");
        
    }

    public Task Unsubscribe(string subscriptionId)
    {
        _subscriptions.Remove(subscriptionId);
        return Task.CompletedTask;
    }

    private static void DefaultHandleTextMessage<T>(T message)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("You got this message: " + message!.ToString());
        Console.ResetColor();
    }

}