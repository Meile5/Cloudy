using PaymentService.DTOs;
using PaymentService.Interfaces;
using PaymentService.Interfaces.Services;
using Shared.Events;

namespace PaymentService.Handlers;

public class StartPaymentHandler(IPaymentService service, IAirlineClient client) : IEventHandler<StartPaymentEvent>
{
    public async Task HandleAsync(StartPaymentEvent message, CancellationToken ct)
    {
        try
        {
            var payment = new CreatePaymentDto
            {
                CardNumber = message.CardNumber,
                Amount = message.Amount,
                Currency = message.Currency
            };

            var finalPayment = await service.AddPayment(payment);

            var successEvent = new PaymentSuccessEvent
            {
                SagaId = message.SagaId,
                PaymentId = Guid.Parse(finalPayment.Id),
                Message = "We did it!!!",
                BookingReference = message.BookingReference,
                PassengerId = message.PassengerId,
                FlightId = message.FlightId,
                SeatId = message.SeatId
            };

            await client.Publish<PaymentSuccessEvent>(successEvent, ct);
        }
        catch (Exception e)
        {
            var failEvent = new PaymentFailEvent
            {
                SagaId = message.SagaId,
                BookingId = message.SagaId,
                Message = "welp... we tried",
                PassengerId = message.PassengerId,
                FlightId = message.FlightId,
                SeatId = message.FlightId
            };
            await client.Publish<PaymentFailEvent>(failEvent, ct);
        }
        
        
    }
}