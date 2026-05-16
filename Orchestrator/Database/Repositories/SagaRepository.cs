using AirlinesBookingSystem.Interfaces.Repositories;
using Orchestrator.Models;

namespace Orchestrator.Database.Repositories;


public class SagaRepository(SagaContext _context) : ISagaRepository
{
    public async Task<Saga> GetById(Guid sagaId)
    {
        var saga = await _context.Sagas.FindAsync(sagaId);
        return saga!;
    }

    public async Task Save(Saga state)
    {
        _context.Sagas.Add(state);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(Saga state)
    {
        if (state.IsCompleted)
        {
            state.IsCompleted = true;
            //if we're saying it's completed, then we assume the steps are completetd
            state.PaymentProcessed = true;
            state.BookingProcessed = true;
            state.CompletedAt = DateTime.Now;
        }
        
        _context.Sagas.Update(state);
        await _context.SaveChangesAsync();
    }

    //pass saga object instead
    /*public async Task Update(string sagaId, bool isFailed, bool? IsCompleted)
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
    }*/
    
}