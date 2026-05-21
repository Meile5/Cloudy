using AirlinesBookingSystem.Interfaces.Services;
using Shared.Events;

namespace AirlinesBookingSystem.Handlers;

public class RevertBookingHandler(IBookingService service) : IEventHandler<RevertBookingCommand>
{
    public async Task HandleAsync(RevertBookingCommand message, CancellationToken ct)
    {
        await service.DeleteBooking(message.BookingId.ToString());
    }
}