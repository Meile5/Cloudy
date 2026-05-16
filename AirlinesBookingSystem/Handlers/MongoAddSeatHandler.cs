using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using AirlinesBookingSystem.Events;

namespace AirlinesBookingSystem.Handlers;

public class MongoAddSeatHandler(IMongoFlightRepository repo ) : IEventHandler<MongoAddSeatCommand>
{
    public async Task HandleAsync(MongoAddSeatCommand message, CancellationToken ct)
    {
        MongoSeats seat = new MongoSeats()
        {
            Id = message.seatId,
            CabinClass = message.CabinClass,
            FareClass = message.FareClass ?? null,
            Price = message.Price,
            SeatNumber = message.SeatNumber,
        };

        await repo.AddAvailableSeat(message.flightId, seat);
    }
}