namespace AirlinesBookingSystem.Events;

public class PaymentSuccessStartBookingEvent
{
    public Guid SagaId { get; set; }
    public string Message { get; set; }
    
    public string BookingReference { get; set; } = null!;
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    
}