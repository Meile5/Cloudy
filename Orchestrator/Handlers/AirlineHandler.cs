namespace Orchestrator.Handlers;

public record AirlineHandler<T> (Action<T> Handler);