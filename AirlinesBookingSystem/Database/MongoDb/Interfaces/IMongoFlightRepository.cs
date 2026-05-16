using AirlinesBookingSystem.Database.MongoDb.Models;

namespace AirlinesBookingSystem.Database.MongoDb.Interfaces;

public interface IMongoFlightRepository
{
    public Task<MongoFlights> CreateFlight(MongoFlights flight);
    
    public Task<MongoFlights> GetFlightById(string id);

    public Task<List<MongoFlights>> GetAllPosts();

    public Task<List<MongoFlights>> GetSoonestFlights(int limit = 15);

    public Task UpdatePost(MongoFlights flight);

    public Task AddAvailableSeat(string flightId, MongoSeats seat);

    public Task UpsertAvailableSeat(string flightId, MongoSeats seat);

    public Task DeleteAvailableSeat(string flightId, string seatId);

    public Task DeletePost(string id);
}