using MongoReadModel.MongoDb.Interfaces;
using MongoReadModel.MongoDb.Models;

namespace MongoReadModel.MongoDb.Services;

public class MongoFlightService(IMongoFlightRepository repo) : IMongoFlightService
{
    public async Task<MongoFlights> CreateFlight(MongoFlights flight)
    {
        return await repo.CreateFlight(flight);
    }

    public async Task<MongoFlights> GetFlightById(string id)
    {
        return await repo.GetFlightById(id);
    }

    public async Task<List<MongoFlights>> GetAllFlights()
    {
        return await repo.GetAllFlights();
    }

    public async Task UpdateFlight(MongoFlights flight)
    {
        await repo.UpdateFlight(flight);
    }
    
    public async Task UpsertAvailableSeat(string flightId, MongoSeats seat)
    {
        await repo.UpsertAvailableSeat(flightId, seat);
    }
    
    public async Task DeleteAvailableSeat(string flightId, string seatId)
    {
        await repo.DeleteAvailableSeat(flightId, seatId);
    }

    public async Task DeleteFlight(string id)
    {
        await repo.DeleteFlight(id);
    }
}