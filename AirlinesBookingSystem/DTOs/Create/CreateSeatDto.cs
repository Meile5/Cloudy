using AirlinesBookingSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace AirlinesBookingSystem.DTOs.Create;

public class CreateSeatDto
{
    
    public string Id { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public static Seat ToSeat(CreateSeatDto dto)
    {
        return new Seat
        {
            Id = dto.Id,
            FlightId = dto.FlightId,
            SeatNumber = dto.SeatNumber,
            CabinClass = dto.CabinClass,
            FareClass = dto.FareClass.IsNullOrEmpty() ? "" : dto.FareClass,
            Status = dto.Status,
            Price = dto.Price,
            CreatedAt = dto.CreatedAt ?? DateTime.Now,
            UpdatedAt = dto.UpdatedAt ?? DateTime.Now
        };
    }
    
    public static CreateSeatDto FromSeat(Seat seat)
    {
        return new CreateSeatDto()
        {
            Id = seat.Id,
            FlightId = seat.FlightId,
            SeatNumber = seat.SeatNumber,
            CabinClass = seat.CabinClass,
            FareClass = seat.FareClass,
            Status = seat.Status,
            Price = seat.Price,
            CreatedAt = seat.CreatedAt,
            UpdatedAt = seat.UpdatedAt
        };
    }
    

}