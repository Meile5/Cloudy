namespace MongoReadModel.Handlers;

public interface IEventHandler <TMessage>
{
    Task HandleAsync(TMessage message, CancellationToken ct);
}