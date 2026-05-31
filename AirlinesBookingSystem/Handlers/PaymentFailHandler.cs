using AirlinesBookingSystem.Interfaces;
using Shared.Events;

namespace AirlinesBookingSystem.Handlers;

public class PaymentFailHandler(
    ISeatLockService seatLockService) 
    : IEventHandler<PaymentFailReleaseSeatEvent>
{
    public async Task HandleAsync(PaymentFailReleaseSeatEvent message, CancellationToken ct)
    {
        await seatLockService.ReleaseSeatAsync(message.FlightId, message.SeatId, message.SagaId.ToString());
    }
}