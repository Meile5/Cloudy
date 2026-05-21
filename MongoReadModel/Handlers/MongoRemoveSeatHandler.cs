using MongoReadModel.MongoDb.Interfaces;
using Shared.Events;

namespace MongoReadModel.Handlers;

public class MongoRemoveSeatHandler(IMongoFlightService service) : IEventHandler<MongoRemoveSeatCommand>
{
    public async Task HandleAsync(MongoRemoveSeatCommand message, CancellationToken ct)
    {
        await service.DeleteAvailableSeat(message.flightId, message.seatId);
    }
}