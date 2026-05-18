namespace Shared.Events;

public class MongoAddSeatCommand
{ 
    public string flightId { get; set; }
    
    public string seatId { get; set; }
    
    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }
    
    public string Status { get; set; } = null!;

    public decimal Price { get; set; }
    
    
}