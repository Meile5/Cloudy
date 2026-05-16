using AirlinesBookingSystem.Database.MongoDb.Models;

namespace AirlinesBookingSystem.Database.MongoDb.Interfaces;

public interface IMongoSeatService
{
    public Task<MongoSeats> CreateSeat(MongoSeats seat);

    public Task<MongoSeats> GetSeatById(string id);

    public Task<List<MongoSeats>> GetAllSeats();

    public Task UpdateSeat(MongoSeats seat);

    public Task DeleteSeat(string id);
}