using Orchestrator.Models;

namespace AirlinesBookingSystem.Interfaces.Repositories;


public class SagaRepository(MessageContext _context) : ISagaRepository
{
    public async Task<SagaState> GetById(Guid sagaId)
    {
        var saga = await _context.MessageSagaStates.FindAsync(sagaId);
        return saga;
    }

    public async Task Save(MessageSagaState state)
    {
        _context.MessageSagaStates.Add(state);
        await _context.SaveChangesAsync();
    }

    //pass saga object instead
    public async Task Update(string sagaId, bool isFailed, bool? IsCompleted)
    {
        var saga = _context.MessageSagaStates.Where(s => s.SagaId.ToString() == sagaId)
            .FirstOrDefault();

        saga.IsFailed = isFailed;
        if (IsCompleted != null && IsCompleted == true)
        {
            saga.IsCompleted = true;
            saga.FileProcessed = true;
            saga.MessageProcessed = true;
            saga.CompletedAt = DateTime.Now;
        }
        _context.MessageSagaStates.Update(saga);
        await _context.SaveChangesAsync();
    }
    
}