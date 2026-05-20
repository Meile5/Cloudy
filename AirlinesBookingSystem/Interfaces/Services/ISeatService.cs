using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.DTOs.Update;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Services;

public interface ISeatService
{
    public  Task<List<Seat>> GetAllSeats();
    
    public Task<Seat> GetSeatById(string seatId);

    public  Task AddSeat(CreateSeatDto seat);

    public  Task UpdateSeat(UpdateSeatDto seat);

    public Task DeleteSeat(string seatId);
}