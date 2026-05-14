using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Services;

namespace AirlinesBookingSystem.Handlers;

public class PaymentSuccessHandler (IBookingService service, IAirlineClient client): IEventHandler<PaymentSuccessStartBookingEvent>
{
    public async Task HandleAsync(PaymentSuccessStartBookingEvent message, CancellationToken ct)
    {
        var bookingDto = new CreateBookingDto
        {
            FlightId = message.FlightId,
            PassengerId = message.PassengerId,
            BookingReference = message.BookingReference,
            
        };
        try
        {
            await service.AddBooking(bookingDto);
            var bookingSuccessEvent = new BookingSuccessEvent
            {
                SagaId = message.SagaId,
                Message = "success",
            };
            client.Publish<BookingSuccessEvent>(bookingSuccessEvent);
            
        }
        catch(Exception ex)
        {
            var bookingFailEvent = new BookingFailEvent
            {
                SagaId = message.SagaId,
                Message = ex.Message,
            };
            client.Publish<BookingFailEvent>(bookingFailEvent);
        }
    }
}