using PaymentService.Interfaces.Services;
using Shared.Events;

namespace PaymentService.Handlers;

public class RefundHandler(IPaymentService service) : IEventHandler<RefundPaymentEvent>
{
    public async Task HandleAsync(RefundPaymentEvent message, CancellationToken ct)
    {
        var paymentToRefund = await service.GetPaymentById(message.PaymentId.ToString());

        if (paymentToRefund != null)
        {
            await service.RefundPayment(paymentToRefund);
        }
    }
}