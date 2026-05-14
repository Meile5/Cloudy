using AirlinesBookingSystem.Adapters;
using AirlinesBookingSystem.Configuration;
using AirlinesBookingSystem.Interfaces;
using EasyNetQ;

namespace AirlinesBookingSystem.Factories;

public static class RabbitMqFactory
{
    private static AirlinesAdapter CreateAdapter(ClientOptions options)
    {
        IBus bus = RabbitHutch.CreateBus(options.ConnectionString);
        return new AirlinesAdapter(bus);
    }

    public static IAirlineClient CreateMessageClient(ClientOptions options)
    {
        AirlinesAdapter adapter = CreateAdapter(options);
        return new Implementation.AirlineClient(adapter);
    }
}