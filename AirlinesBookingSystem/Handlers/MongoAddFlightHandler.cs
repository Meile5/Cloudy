using AirlinesBookingSystem.Database.MongoDb.Interfaces;
using AirlinesBookingSystem.Database.MongoDb.Models;
using Shared.Events;

namespace AirlinesBookingSystem.Handlers;

public class MongoAddFlightHandler(IMongoFlightService service) : IEventHandler<MongoAddFlightCommand>
{
    public async Task HandleAsync(MongoAddFlightCommand message, CancellationToken ct)
    {
        var mongoFlight = new MongoFlights
        {
            Id = message.Id,
            FlightNumber = message.FlightNumber,
            OriginAirport = message.OriginAirport,
            DestinationAirport = message.DestinationAirport,
            DepartureTime = message.DepartureTime,
            ArrivalTime = message.ArrivalTime,
            AircraftId = message.AircraftId,
            Status = message.Status,
            Currency = message.Currency,
            AvailableSeats = []
        };

        await service.CreateFlight(mongoFlight);

    }
}