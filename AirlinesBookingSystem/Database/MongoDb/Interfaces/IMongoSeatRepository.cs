using AirlinesBookingSystem.Database.MongoDb.Models;

namespace AirlinesBookingSystem.Database.MongoDb.Interfaces;

public interface IMongoSeatRepository
{
    public Task<MongoSeats> CreateSeat(MongoSeats blog);
    
    public Task<MongoSeats> GetSeatById(string id);

    public Task<List<MongoSeats>> GetAllSeats();

    public Task UpdateSeat(MongoSeats blog);

    public Task DeleteSeat(string id);

}