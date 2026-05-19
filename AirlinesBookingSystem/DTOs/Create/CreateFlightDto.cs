using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.DTOs.Create;

public class CreateFlightDto
{
    public string FlightNumber { get; set; } = null!;

    public string OriginAirport { get; set; } = null!;

    public string DestinationAirport { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string? AircraftId { get; set; }

    public string? Status { get; set; }

    public decimal BaseFare { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public static Flight ToFlight(CreateFlightDto dto)
    {
        return new Flight
        {
            Id = Guid.NewGuid().ToString(),
            FlightNumber = dto.FlightNumber,
            OriginAirport = dto.OriginAirport,
            DestinationAirport = dto.DestinationAirport,
            DepartureTime = dto.DepartureTime,
            ArrivalTime = dto.ArrivalTime,
            AircraftId = dto.AircraftId,
            Status = dto.Status,
            BaseFare = dto.BaseFare,
            Currency = dto.Currency,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt
        };
    }
    
    public static CreateFlightDto FromFlight(Flight flight)
    {
        return new CreateFlightDto()
        {
            FlightNumber = flight.FlightNumber,
            OriginAirport = flight.OriginAirport,
            DestinationAirport = flight.DestinationAirport,
            DepartureTime = flight.DepartureTime,
            ArrivalTime = flight.ArrivalTime,
            AircraftId = flight.AircraftId,
            Status = flight.Status,
            BaseFare = flight.BaseFare,
            Currency = flight.Currency,
            CreatedAt = flight.CreatedAt,
            UpdatedAt = flight.UpdatedAt
        };
    }
    
}