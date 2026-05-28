using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.DTOs.Update;

public class UpdateSeatDto
{
    public string Id { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }

    public static Seat toSeat(UpdateSeatDto dto)
    {
        return new Seat
        {
            Id = dto.Id,
            FlightId = dto.FlightId,
            SeatNumber = dto.SeatNumber,
            CabinClass = dto.CabinClass,
            FareClass = dto.FareClass,
            Status = dto.Status,
            Price = dto.Price,
            UpdatedAt = DateTime.Now
        };
    }
    
    public static Seat UpdateSeat(Seat seat, UpdateSeatDto dto)
    {
        seat.SeatNumber = dto.SeatNumber;
        seat.CabinClass = dto.CabinClass;
        seat.FareClass = dto.FareClass;
        seat.Status = dto.Status;
        seat.Price = dto.Price;
        seat.UpdatedAt = DateTime.Now;
        return seat;
    }
    
}