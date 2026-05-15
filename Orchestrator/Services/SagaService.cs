using AirlinesBookingSystem.Interfaces.Repositories;
using Orchestrator.Interfaces.Services;
using Orchestrator.Models;

namespace Orchestrator.Services;

public class SagaService(ISagaRepository repo) : ISagaService
{
    public async Task<Saga> GetById(Guid sagaId)
    {
        return await repo.GetById(sagaId);
    }

    public async Task Save(SagaState state)
    {
        var newState = SagaState.toSagaObject(state);
        await repo.Save(newState);
    }
    
    public async Task Update(SagaState state)
    {
        var newState = SagaState.toSagaObject(state);
        await repo.Update(newState);
    }
}