namespace AirlinesBookingSystem.Handlers;

public record EventHandler<T> (Action<T> Handler);