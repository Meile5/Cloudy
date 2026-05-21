namespace MongoReadModel.Handlers;

public record AirlineHandler<T> (Action<T> Handler);