using System;
using System.Collections.Generic;

namespace Orchestrator.Models;

public partial class Saga
{
    public string SagaId { get; set; } = null!;

    public string? BookingId { get; set; }

    public string? PaymentId { get; set; }

    public bool PaymentProcessed { get; set; }

    public bool BookingProcessed { get; set; }

    public bool IsFailed { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }
}
