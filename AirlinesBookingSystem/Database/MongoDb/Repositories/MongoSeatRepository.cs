using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using MongoDB.Driver;

namespace AirlinesBookingSystem.Database.MongoDb.Repositories;

public class MongoSeatRepository : IMongoSeatRepository
{
    private readonly IMongoCollection<MongoSeats> _seats;
    
    public MongoSeatRepository(IMongoDatabase db)
    {
        _seats = db.GetCollection<MongoSeats>("seats");
    }
    
    public async Task<MongoSeats> CreateSeat(MongoSeats seat)
    {
        await _seats.InsertOneAsync(seat);
        return seat;
    }

    public async Task<MongoSeats> GetSeatById(string id)
    {
        return await _seats.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<MongoSeats>> GetAllSeats()
    {
        return await _seats.Find(_ => true).ToListAsync();
    }
    
    public async Task UpdateSeat(MongoSeats seat)
    {
        await _seats.ReplaceOneAsync(s => s.Id == seat.Id, seat);
    }
    
    public async Task DeleteSeat(string id)
    {
        await _seats.DeleteOneAsync(s => s.Id == id);
    }

}