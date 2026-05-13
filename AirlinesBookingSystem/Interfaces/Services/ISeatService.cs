using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Services;

public interface ISeatService
{
    public  Task<List<Seat>> GetAllSeats();
    
    public Task<Seat> GetSeatById(string seatId);

    public  Task AddSeat(Seat passenger);

    public  Task UpdateSeat(Seat passenger);

    public Task DeleteSeat(string seatId);
}