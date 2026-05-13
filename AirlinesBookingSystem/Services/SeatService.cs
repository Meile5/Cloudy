using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class SeatService(ISeatRepository repo) : ISeatService
{
    public async Task<List<Seat>> GetAllSeats()
    {
        return await repo.GetAllSeats();
    }
    
    public async Task<Seat> GetSeatById(string seatId)
    {
        try
        {
            return await repo.GetSeatById(seatId);
        }
        catch (Exception e)
        {
            Console.WriteLine("probably couldn't find passenger with that id :(");
            Console.WriteLine(e.StackTrace);
            throw;
        }
        
    }
    
    public async Task AddSeat(CreateSeatDto seat)
    {
        var newSeat = CreateSeatDto.ToSeat(seat);
        await repo.AddSeat(newSeat);
    }
    
    public async Task UpdateSeat(Seat seat)
    {
        await repo.UpdateSeat(seat);
    }
    
    public async Task DeleteSeat(string seatId)
    {
        await repo.DeleteSeat(seatId);
    }
}