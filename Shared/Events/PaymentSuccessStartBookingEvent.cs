namespace Shared.Events;

public class PaymentSuccessStartBookingEvent
{
    public Guid SagaId { get; set; }
    
    public Guid PaymentId { get; set; }
    public string Message { get; set; }
    
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    public string SeatId { get; set; } = null!; 

}