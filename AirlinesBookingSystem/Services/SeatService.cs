using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class SeatService(ISeatRepository repo, IAirlineClient client) : ISeatService
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
        if (seat.Status == "available")
        {
            var command = new MongoAddSeatCommand
            {
                flightId = seat.FlightId,
                seatId = seat.Id,
                SeatNumber = seat.SeatNumber,
                CabinClass = seat.CabinClass,
                FareClass = seat.FareClass ?? null,
                Price = seat.Price
            };
            await client.Publish<MongoAddSeatCommand>(command);
        } else if (seat.Status == "sold")
        {
            var command = new MongoRemoveSeatCommand
            {
                flightId = seat.FlightId,
                seatId = seat.Id
            };

            await client.Publish<MongoRemoveSeatCommand>(command);
        }
        
        await repo.UpdateSeat(seat);
    }
    
    public async Task DeleteSeat(string seatId)
    {
        await repo.DeleteSeat(seatId);
    }
}