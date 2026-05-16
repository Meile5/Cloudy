using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Services;

namespace AirlinesBookingSystem.Handlers;
public class PaymentSuccessHandler(
    IBookingService service, 
    IAirlineClient client,
    ISeatLockService seatLockService) 
    : IEventHandler<PaymentSuccessStartBookingEvent>
{
    public async Task HandleAsync(PaymentSuccessStartBookingEvent message, CancellationToken ct)
    {
        var bookingDto = new CreateBookingDto
        {
            FlightId = message.FlightId,
            PassengerId = message.PassengerId,
        };

        try
        {
            await service.AddBooking(bookingDto);
            // release lock
            await seatLockService.ReleaseSeatAsync(message.FlightId, message.SeatId);

            await client.Publish(new BookingSuccessEvent
            {
                SagaId = message.SagaId,
                Message = "success",
            });
        }
        catch (Exception ex)
        {
            await seatLockService.ReleaseSeatAsync(message.FlightId, message.SeatId);
            await client.Publish(new BookingFailEvent
            {
                SagaId = message.SagaId,
                Message = ex.Message,
            });
        }
    }
}