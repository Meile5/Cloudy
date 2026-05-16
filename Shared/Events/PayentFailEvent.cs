using System.Runtime.InteropServices.JavaScript;

namespace AirlinesBookingSystem.Events;

public class PayentFailEvent
{
    public Guid SagaId { get; set; }
    public Guid BookingId { get; set; }
    public string Message { get; set; }
    public decimal Amount { get; set; }
    
}