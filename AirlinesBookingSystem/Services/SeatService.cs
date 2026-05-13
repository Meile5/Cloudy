using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class SeatService(ISeatRepository repo)
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
    
    public async Task AddSeat(Seat passenger)
    {
        await repo.AddSeat(passenger);
    }
    
    public async Task UpdateSeat(Seat passenger)
    {
        await repo.UpdateSeat(passenger);
    }
    
    public async Task DeleteSeat(string seatId)
    {
        await repo.DeleteSeat(seatId);
    }
}