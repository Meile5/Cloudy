namespace AirlinesBookingSystem.Events;

public class BookingStartedEvent
{
    public string BookingReference { get; set; } = null!;
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    
    public decimal Amount  {get; set;}
}