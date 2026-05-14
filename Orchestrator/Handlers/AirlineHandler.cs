namespace AirlinesBookingSystem.Handlers;

public record AirlineHandler<T> (Action<T> Handler);