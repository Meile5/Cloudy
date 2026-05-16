using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.DTOs.Create;

public class CreateBookingDto
{
    public string BookingReference { get; set; } = null!;
    
    public string PassengerId { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public decimal Price { get; set; }
    
    public string SeatId { get; set; } = null!;
    
    public static Booking ToBooking(CreateBookingDto dto)
    {
        return new Booking
        {
            Id = Guid.NewGuid().ToString(),
            BookingReference = dto.BookingReference,
            FlightId = dto.FlightId,
            SeatId = dto.SeatId,
            PassengerId = dto.PassengerId,
        };
    }
    
    public static CreateBookingDto FromBooking(Booking booking)
    {
        return new CreateBookingDto
        {
            FlightId = booking.FlightId,
            BookingReference = booking.BookingReference,
            SeatId = booking.SeatId,
            PassengerId = booking.PassengerId,
        };
    }


}