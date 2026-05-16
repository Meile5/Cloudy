namespace AirlinesBookingSystem.Events;

public class MongoAddSeatCommand
{ 
    public string flightId { get; set; }
    
    public string seatId { get; set; }
    
    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }

    public decimal Price { get; set; }
    
    
}