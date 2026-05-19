using AirlinesBookingSystem.Interfaces;
using EasyNetQ;
using PaymentService.Adapters;
using PaymentService.Configuration;
using PaymentService.Implementation;
using PaymentService.Interfaces;

namespace PaymentService.Factories;

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