namespace PaymentService.Handlers;

public record AirlineHandler<T> (Action<T> Handler);