namespace Shared.Events;

public class BookingFailEvent
{
    public Guid SagaId { get; set; }
    public Guid PaymentId { get; set; }
    public string Message { get; set; }
    
}