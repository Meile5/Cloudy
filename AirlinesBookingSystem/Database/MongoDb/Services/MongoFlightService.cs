using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using AirlinesBookingSystem.Interfaces.Repositories;
using MongoDB.Driver;

namespace AirlinesBookingSystem.Database.MongoDb.Services;

public class MongoFlightService(IMongoFlightRepository repo)
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
    
    public async Task<List<MongoFlights>> GetSoonestFlights(int limit = 15)
    {
        return await repo.GetSoonestFlights(limit);
    }

    public async Task UpdatePost(MongoFlights flight)
    {
        await repo.UpdatePost(flight);
    }

    public async Task AddAvailableSeat(string flightId, MongoSeats seat)
    {
        await repo.AddAvailableSeat(flightId, seat);
    }
    
    public async Task UpsertAvailableSeat(string flightId, MongoSeats seat)
    {
        await repo.UpsertAvailableSeat(flightId, seat);
    }
    
    public async Task DeleteAvailableSeat(string flightId, string seatId)
    {
        await repo.DeleteAvailableSeat(flightId, seatId);
    }

    public async Task DeletePost(string id)
    {
        await repo.DeletePost(id);
    }
}