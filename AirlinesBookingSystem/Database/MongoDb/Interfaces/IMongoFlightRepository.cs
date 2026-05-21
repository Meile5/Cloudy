using AirlinesBookingSystem.Database.MongoDb.Models;

namespace AirlinesBookingSystem.Database.MongoDb.Interfaces;

public interface IMongoFlightRepository
{
    public Task<MongoFlights> CreateFlight(MongoFlights flight);
    
    public Task<MongoFlights> GetFlightById(string id);

    public Task<List<MongoFlights>> GetAllFlights();

    public Task UpdateFlight(MongoFlights flight);

    public Task UpsertAvailableSeat(string flightId, MongoSeats seat);

    public Task DeleteAvailableSeat(string flightId, string seatId);

    public Task DeleteFlight(string id);
}