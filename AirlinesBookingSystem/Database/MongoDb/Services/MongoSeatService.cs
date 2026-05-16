using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using AirlinesBookingSystem.Interfaces.Repositories;

namespace AirlinesBookingSystem.Database.MongoDb.Services;

public class MongoSeatService (IMongoSeatRepository repo) : IMongoSeatService
{
    public async Task<MongoSeats> CreateSeat(MongoSeats seat)
    {
        return await repo.CreateSeat(seat);
    }

    public async Task<MongoSeats> GetSeatById(string id)
    {
        return await repo.GetSeatById(id);
    }

    public async Task<List<MongoSeats>> GetAllSeats()
    {
        return await repo.GetAllSeats();
    }
    
    public async Task UpdateSeat(MongoSeats seat)
    {
        await repo.UpdateSeat(seat);
    }
    
    public async Task DeleteSeat(string id)
    {
        await repo.DeleteSeat(id);
    }
}