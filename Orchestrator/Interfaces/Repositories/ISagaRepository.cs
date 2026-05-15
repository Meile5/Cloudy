using Orchestrator.Models;

namespace AirlinesBookingSystem.Interfaces.Repositories;

public interface ISagaRepository
{
    public Task<Saga> GetById(Guid sagaId);

    public Task Save(Saga state);

    public Task Update(Saga state);
}