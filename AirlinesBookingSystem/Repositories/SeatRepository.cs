using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesBookingSystem.Repositories;

public class SeatRepository(BookingContext context) : ISeatRepository
{
    public async Task<List<Seat>> GetAllSeats()
    {
        return await context.Seats.ToListAsync();
    }
    
    public async Task<Seat> GetSeatById(string seatId)
    {
        return await context.Seats.Where(s => s.Id == seatId).FirstOrDefaultAsync();
    }
    
    public async Task AddSeat(Seat seat)
    {
        context.Seats.Add(seat);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateSeat(Seat seat)
    {
        context.Seats.Update(seat);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteSeat(string seatId)
    {
        var toDelete = await context.Seats.Where(s => s.Id == seatId).FirstOrDefaultAsync();
        context.Seats.Remove(toDelete!);
        await context.SaveChangesAsync();
    }
}