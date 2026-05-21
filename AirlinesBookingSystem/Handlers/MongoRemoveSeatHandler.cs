using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using Shared.Events;

namespace AirlinesBookingSystem.Handlers;

public class MongoRemoveSeatHandler(IMongoFlightService service) : IEventHandler<MongoRemoveSeatCommand>
{
    public async Task HandleAsync(MongoRemoveSeatCommand message, CancellationToken ct)
    {
        await service.DeleteAvailableSeat(message.flightId, message.seatId);
    }
}