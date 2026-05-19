using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Shared.Events;

namespace AirlinesBookingSystem.Services;

public class FlightService(IFlightRepository repo, IAirlineClient client) : IFlightService
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
    
    public async Task AddFlight(CreateFlightDto flight)
    {
        var newFlight = CreateFlightDto.ToFlight(flight);
        await repo.AddFlight(newFlight);

        var command = new MongoAddFlightCommand
        {
            Id = newFlight.Id,
            FlightNumber = newFlight.FlightNumber,
            OriginAirport = newFlight.OriginAirport,
            DestinationAirport = newFlight.DestinationAirport,
            DepartureTime = newFlight.DepartureTime,
            ArrivalTime = newFlight.ArrivalTime,
            AircraftId = newFlight.AircraftId,
            Status = newFlight.Status,
            Currency = newFlight.Currency
        };
        await client.Publish<MongoAddFlightCommand>(command);
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