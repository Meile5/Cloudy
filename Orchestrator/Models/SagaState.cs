namespace Orchestrator.Models;

public class SagaState
{
    public Guid SagaId { get; set; }
    public Guid? BookingId { get; set; }
    public Guid? PaymentId { get; set; }

    public bool BookingProcessed { get; set; }
    public bool PaymentProcessed { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public static Saga toSagaObject(SagaState sagaState)
    {
        return new Saga
        {
            SagaId = sagaState.SagaId.ToString(),
            BookingId = sagaState.BookingId.ToString() ?? null,
            PaymentId = sagaState.PaymentId.ToString() ?? null,
            PaymentProcessed = sagaState.PaymentProcessed,
            BookingProcessed = sagaState.BookingProcessed,
            IsFailed = sagaState.IsFailed,
            IsCompleted = sagaState.IsCompleted,
            CreatedAt = sagaState.StartedAt,
            CompletedAt = sagaState.CompletedAt ?? null
        };
    }
}