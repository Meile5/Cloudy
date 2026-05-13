using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesFlightsystem.Repositories;

public class FlightRepository(BookingContext context)
{
    public async Task<List<Flight>> GetAllFlights()
    {
        return await context.Flights.ToListAsync();
    }
    
    public async Task<Flight> GetFlightById(string flightId)
    {
        return await context.Flights.Where(f => f.Id == flightId).FirstOrDefaultAsync();
    }
    
    public async Task AddFlight(Flight flight)
    {
        context.Flights.Add(flight);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateFlight(Flight flight)
    {
        context.Flights.Update(flight);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteFlight(string flightId)
    {
        var toDelete = await context.Flights.Where(f => f.Id == flightId).FirstOrDefaultAsync();
        context.Flights.Remove(toDelete);
        await context.SaveChangesAsync();
    }
}