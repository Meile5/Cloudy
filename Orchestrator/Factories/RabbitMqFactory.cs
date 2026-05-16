using AirlinesBookingSystem.Interfaces;
using EasyNetQ;
using Orchestrator.Adapters;
using Orchestrator.Configuration;
using Orchestrator.Implementation;
using Orchestrator.Interfaces;

namespace Orchestrator.Factories;

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
        return new AirlineClient(adapter);
    }
}