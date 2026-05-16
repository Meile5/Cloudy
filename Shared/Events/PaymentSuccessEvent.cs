namespace AirlinesBookingSystem.Events;

public class PaymentSuccessEvent
{
    public Guid SagaId { get; set; }
    
    public Guid PaymentId { get; set; }
    public string Message { get; set; }
    
    public string BookingReference { get; set; } = null!;
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    public string SeatId { get; set; } = null!;
    
}