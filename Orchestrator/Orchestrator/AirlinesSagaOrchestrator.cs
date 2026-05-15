using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Handlers;
using AirlinesBookingSystem.Interfaces;
using Orchestrator.Interfaces.Services;
using Orchestrator.Models;

namespace Orchestrator.Orchestrator;

public class AirlinesSagaOrchestrator :
    IEventHandler<BookingFailEvent>,
    IEventHandler<BookingStartedEvent>,
    IEventHandler<BookingSuccessEvent>,
    IEventHandler<StartPaymentEvent>,
    IEventHandler<PayentFailEvent>,
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
        throw new NotImplementedException();
    }

    public async Task HandleAsync(BookingStartedEvent booking, CancellationToken ct)
    {
        var sagaId = Guid.NewGuid();
        var state = new SagaState
        {
            SagaId = sagaId,
            BookingProcessed = false,
            PaymentProcessed = false,
            IsCompleted = false,
            IsFailed = false,
        };
        await _service.Save(state);
        
        await _airlineClient.Publish(new StartPaymentEvent()
        {
            SagaId = sagaId,
            BookingReference = booking.BookingReference,
            PassengerId = booking.PassengerId,
            FlightId = booking.FlightId,
            Amount = booking.Amount
            
        });
    }

    public async Task HandleAsync(BookingSuccessEvent message, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(StartPaymentEvent message, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(PayentFailEvent message, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task HandleAsync(PaymentSuccessEvent message, CancellationToken ct)
    {
        await _airlineClient.Publish(new PaymentSuccessStartBookingEvent()
        {
            SagaId = message.SagaId,
            Message = message.Message,
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