namespace Shared.Events;

public class MongoAddFlightCommand
{
    public string Id { get; set; } = null!;
    
    public string FlightNumber { get; set; } = null!;

    public string OriginAirport { get; set; } = null!;

    public string DestinationAirport { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }
    
    public string? AircraftId { get; set; }

    public string? Status { get; set; }

    public string Currency { get; set; } = null!;
}