using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesBookingSystem.Repositories;

public class PassengerRepository(BookingContext context) : IPassengerRepository
{
    public async Task<List<Passenger>> GetAllPassengers()
    {
        return await context.Passengers.ToListAsync();
    }
    
    public async Task<Passenger> GetPassengerById(string passengerId)
    {
        return await context.Passengers.Where(p => p.Id == passengerId).FirstOrDefaultAsync();
    }
    
    public async Task AddPassenger(Passenger passenger)
    {
        context.Passengers.Add(passenger);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdatePassenger(Passenger passenger)
    {
        context.Passengers.Update(passenger);
        await context.SaveChangesAsync();
    }
    
    public async Task DeletePassenger(string passengerId)
    {
        var toDelete = await context.Passengers.Where(p => p.Id == passengerId).FirstOrDefaultAsync();
        context.Passengers.Remove(toDelete!);
        await context.SaveChangesAsync();
    }
}