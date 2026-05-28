using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.DTOs.Update;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Shared.Events;

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
        
        await UpdateMongoSeats(newSeat);
    }
    
    public async Task UpdateSeat(UpdateSeatDto seat)
    {
        var seatToupdate = await GetSeatById(seat.Id);
        
        seatToupdate = UpdateSeatDto.UpdateSeat(seatToupdate, seat);
        
        await repo.UpdateSeat(seatToupdate);
        
        await UpdateMongoSeats(seatToupdate);
    }
    
    public async Task SellSeat(string seatId)
    {
        var seat = await GetSeatById(seatId);

        seat.Status = "sold";
        seat.UpdatedAt = DateTime.Now;
        
        await repo.UpdateSeat(seat);
        
        await UpdateMongoSeats(seat);
    }

    public async Task UpdateMongoSeats(Seat seat)
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
                Status = seat.Status,
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
    }
    
    public async Task DeleteSeat(string seatId)
    {
        await repo.DeleteSeat(seatId);
    }
}