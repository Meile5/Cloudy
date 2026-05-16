using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using MongoDB.Driver;

namespace AirlinesBookingSystem.Database.MongoDb.Repositories;

public class MongoFlightRepository : IMongoFlightRepository
{
    private readonly IMongoCollection<MongoFlights> _flights;

    public MongoFlightRepository(IMongoDatabase db)
    {
        _flights = db.GetCollection<MongoFlights>("flights");
    }

    public async Task<MongoFlights> CreateFlight(MongoFlights flight)
    {
        await _flights.InsertOneAsync(flight);
        return flight;
    }

    public async Task<MongoFlights> GetFlightById(string id)
    {
        return await _flights.Find(f => f.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<MongoFlights>> GetAllPosts()
    {
        return await _flights.Find(_ => true).ToListAsync();
    }
    
    public async Task<List<MongoFlights>> GetSoonestFlights(int limit = 15)
    {
        return await _flights.Find(_ => true)
            .SortByDescending(flight => flight.DepartureTime)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task UpdatePost(MongoFlights flight)
    {
        await _flights.ReplaceOneAsync(f => f.Id == flight.Id, flight);
    }

    public async Task AddAvailableSeat(string flightId, MongoSeats seat)
    {
        await _flights.UpdateOneAsync(
            f => f.Id == flightId, 
            Builders<MongoFlights>.Update.Push(f => f.AvailableSeats, seat));
    }
    
    public async Task DeleteAvailableSeat(string flightId, string seatId)
    {
        var newpost = await GetFlightById(flightId);

        newpost.AvailableSeats.RemoveAll(s => s.Id == seatId);
        
        await UpdatePost(newpost);

    }

    public async Task DeletePost(string id)
    {
        await _flights.DeleteOneAsync(f => f.Id == id);
    }
}