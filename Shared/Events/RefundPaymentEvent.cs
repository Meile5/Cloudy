namespace Shared.Events;

public class RefundPaymentEvent
{
    public Guid SagaId { get; set; }
    public Guid PaymentId { get; set; }
}