using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using Shared.Events;

namespace AirlinesBookingSystem.Handlers;

public class MongoRemoveSeatHandler(IMongoFlightRepository repo) : IEventHandler<MongoRemoveSeatCommand>
{
    public async Task HandleAsync(MongoRemoveSeatCommand message, CancellationToken ct)
    {
        await repo.DeleteAvailableSeat(message.flightId, message.seatId);
    }
}