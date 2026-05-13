using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Repositories;

public interface IFlightRepository
{
    public Task<List<Flight>> GetAllFlights();

    public Task<Flight> GetFlightById(string flightId);

    public Task AddFlight(Flight flight);

    public Task UpdateFlight(Flight flight);

    public  Task DeleteFlight(string flightId);
}