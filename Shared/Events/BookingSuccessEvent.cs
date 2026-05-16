namespace Shared.Events;

public class BookingSuccessEvent
{
    public Guid SagaId { get; set; }
    public Guid BookingId { get; set; }
    public string Message { get; set; }
    
}