using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AirlinesBookingSystem.Database.MongoDb.Models;

public class MongoFlights
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    public string FlightNumber { get; set; } = null!;

    public string OriginAirport { get; set; } = null!;

    public string DestinationAirport { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? AircraftId { get; set; }

    public string? Status { get; set; }

    public string Currency { get; set; } = null!;

    public List<MongoSeats> AvailableSeats { get; set; } 
}