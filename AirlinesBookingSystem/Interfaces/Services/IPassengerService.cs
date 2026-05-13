using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Services;

public interface IPassengerService
{
    public  Task<List<Passenger>> GetAllPassengers();
    
    public  Task<Passenger> GetPassengerById(string passengerId);
    
    public  Task AddPassenger(Passenger passenger);

    public  Task UpdatePassenger(Passenger passenger);

    public Task DeletePassenger(string passengerId);
}