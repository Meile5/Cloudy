namespace AirlinesBookingSystem.Events;

public class StartPaymentEvent
{
    public Guid SagaId { get; set; }
    public string BookingReference { get; set; } = null!;
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    public string SeatId { get; set; } = null!;
    public decimal Amount { get; set; }
    
}