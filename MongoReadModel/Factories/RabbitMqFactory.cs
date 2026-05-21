using EasyNetQ;
using MongoReadModel.Adapters;
using MongoReadModel.Configuration;
using MongoReadModel.Implementation;
using MongoReadModel.Interfaces;

namespace MongoReadModel.Factories;

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