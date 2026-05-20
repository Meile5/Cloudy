using AirlinesBookingSystem.Interfaces;
using Orchestrator.Handlers;
using Orchestrator.Interfaces;
using Orchestrator.Interfaces.Services;
using Orchestrator.Models;
using Shared.Events;

namespace Orchestrator.Orchestrator;

public class AirlinesSagaOrchestrator :
    IEventHandler<BookingFailEvent>,
    IEventHandler<BookingStartedEvent>,
    IEventHandler<BookingSuccessEvent>,
    IEventHandler<PaymentFailEvent>,
    IEventHandler<PaymentSuccessEvent>
    
{
    private readonly IAirlineClient _airlineClient;
    private readonly ISagaService _service;

    public AirlinesSagaOrchestrator(
        IAirlineClient messageClient,
        ISagaService service
        )
    {
        _airlineClient = messageClient;
        _service = service;
    }
    

   
    public async Task HandleAsync(BookingFailEvent message, CancellationToken ct)
    {
        var command = new RefundPaymentEvent()
        {
            SagaId = message.SagaId,
            PaymentId = message.PaymentId,
        };
        
        await _airlineClient.Publish<RefundPaymentEvent>(command);
    }

    //starting flow
    public async Task HandleAsync(BookingStartedEvent booking, CancellationToken ct)
    {
        var state = new SagaState
        {
            SagaId = booking.SagaId,
            BookingProcessed = false,
            PaymentProcessed = false,
            IsCompleted = false,
            IsFailed = false,
        };
        await _service.Save(state);
        
        await _airlineClient.Publish(new StartPaymentEvent()
        {
            SagaId = booking.SagaId,
            BookingReference = booking.BookingReference,
            PassengerId = booking.PassengerId,
            FlightId = booking.FlightId,
            Amount = booking.Amount,
            SeatId = booking.SeatId,
            CardNumber = booking.CardNumber,
            Currency = booking.Currency,
            
        });
    }

    //booking success
    public async Task HandleAsync(BookingSuccessEvent message, CancellationToken ct)
    {
        var state = new SagaState
        {
            SagaId = message.SagaId,
            BookingProcessed = true,
            PaymentProcessed = true,
            IsCompleted = true,
            IsFailed = false,
        };
        await _service.Update(state);
    }
    

    //payment fail
    public async Task HandleAsync(PaymentFailEvent message, CancellationToken ct)
    {
        await _airlineClient.Publish(new PaymentFailReleaseSeatEvent()
        {
            SagaId = message.SagaId,
            Message = message.Message,
            PassengerId = message.PassengerId,
            FlightId = message.FlightId,
            SeatId = message.SeatId
        });
        var updatedState = new SagaState
        {
            SagaId = message.SagaId,
            BookingProcessed = false,
            PaymentProcessed = false,
            IsCompleted = false,
            IsFailed = true,
            PaymentId = message.PaymentId
        };
        
        await _service.Update(updatedState);
    }

    //payment success
    public async Task HandleAsync(PaymentSuccessEvent message, CancellationToken ct)
    {
        await _airlineClient.Publish(new PaymentSuccessStartBookingEvent()
        {
            SagaId = message.SagaId,
            Message = message.Message,
            PaymentId = message.PaymentId,
            BookingReference = message.BookingReference,
            PassengerId = message.PassengerId,
            FlightId = message.FlightId,
            SeatId = message.SeatId
        });
        var updatedState = new SagaState
        {
            SagaId = message.SagaId,
            BookingProcessed = false,
            PaymentProcessed = true,
            IsCompleted = false,
            IsFailed = false,
            PaymentId = message.PaymentId
        };
        
        await _service.Update(updatedState);
    }
}