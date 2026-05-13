using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Repositories;

public interface ISeatRepository
{
    public Task<List<Seat>> GetAllSeats();

    public Task<Seat> GetSeatById(string seatId);

    public Task AddSeat(Seat seat);

    public Task UpdateSeat(Seat seat);

    public  Task DeleteSeat(string seatId);
}