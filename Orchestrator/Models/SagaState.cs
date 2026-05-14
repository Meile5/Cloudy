namespace Orchestrator.Models;

public class SagaState
{
    public Guid SagaId { get; set; }
    public Guid BookingId { get; set; }
    public Guid PaymentId { get; set; }

    public bool BookingProcessed { get; set; }
    public bool PaymentProcessed { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}