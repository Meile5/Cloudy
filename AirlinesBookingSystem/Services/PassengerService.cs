using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class PassengerService(IPassengerRepository repo)
{
    public async Task<List<Passenger>> GetAllPassengers()
    {
        return await repo.GetAllPassengers();
    }
    
    public async Task<Passenger> GetPassengerById(string passengerId)
    {
        try
        {
            return await repo.GetPassengerById(passengerId);
        }
        catch (Exception e)
        {
            Console.WriteLine("probably couldn't find passenger with that id :(");
            Console.WriteLine(e.StackTrace);
            throw;
        }
        
    }
    
    public async Task AddPassenger(Passenger passenger)
    {
        await repo.AddPassenger(passenger);
    }
    
    public async Task UpdatePassenger(Passenger passenger)
    {
        await repo.UpdatePassenger(passenger);
    }
    
    public async Task DeletePassenger(string passengerId)
    {
        await repo.DeletePassenger(passengerId);
    }
}