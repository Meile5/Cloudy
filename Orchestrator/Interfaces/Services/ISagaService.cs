using Orchestrator.Models;

namespace Orchestrator.Interfaces.Services;

public interface ISagaService
{
    public  Task<Saga> GetById(Guid sagaId);

    public Task Save(SagaState state);

    public  Task Update(SagaState state);
}