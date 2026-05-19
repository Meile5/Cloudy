namespace Shared.Events;

public class PaymentFailReleaseSeatEvent
{
    public Guid SagaId { get; set; }
    public string Message { get; set; }
    public string PassengerId { get; set; } = null!;
    public string FlightId { get; set; } = null!;
    public string SeatId { get; set; } = null!; 
}