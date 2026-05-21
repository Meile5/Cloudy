using MongoDB.Bson.Serialization.Attributes;

namespace MongoReadModel.MongoDb.Models;

public class MongoSeats
{
    [BsonId] 
    //[BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    
    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }
    
}