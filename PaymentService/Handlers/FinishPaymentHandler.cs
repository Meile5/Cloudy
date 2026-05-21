using PaymentService.Interfaces;
using PaymentService.Interfaces.Services;
using Shared.Events;

namespace PaymentService.Handlers;

public class FinishPaymentHandler(IPaymentService service, IAirlineClient client) : IEventHandler<FinishPaymentEvent>
{
    public async Task HandleAsync(FinishPaymentEvent message, CancellationToken ct)
    {
        try
        {
            var payment = await service.GetPaymentById(message.PaymentId.ToString());

            await service.CompletePayment(payment);

            var finalEvent = new PaymentFinalizedEvent
            {
                SagaId = message.SagaId,
                BookingId = message.BookingId,
                PaymentId = message.PaymentId,
                Message = "flow complete :)",
            };
            await client.Publish<PaymentFinalizedEvent>(finalEvent);
        }
        catch (Exception e)
        {
            var finalEvent = new PaymentFinalizedFailEvent
            {
                SagaId = message.SagaId,
                BookingId = message.BookingId,
                PaymentId = message.PaymentId,
                Message = "flow NOT complete :(",
            };
            await client.Publish<PaymentFinalizedFailEvent>(finalEvent);
        }
        
    }
}