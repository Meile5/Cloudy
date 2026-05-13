using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class FlightService(IFlightRepository repo) : IFlightService
{
    public async Task<List<Flight>> GetAllFlights()
    {
        return await repo.GetAllFlights();
    }
    
    public async Task<Flight> GetFlightById(string flightId)
    {
        try
        {
            return await repo.GetFlightById(flightId);
        }
        catch (Exception e)
        {
            Console.WriteLine("probably couldn't find passenger with that id :(");
            Console.WriteLine(e.StackTrace);
            throw;
        }
        
    }
    
    public async Task AddFlight(Flight flight)
    {
        await repo.AddFlight(flight);
    }
    
    public async Task UpdateFlight(Flight flight)
    {
        await repo.UpdateFlight(flight);
    }
    
    public async Task DeleteFlight(string flightid)
    {
        await repo.DeleteFlight(flightid);
    }
}