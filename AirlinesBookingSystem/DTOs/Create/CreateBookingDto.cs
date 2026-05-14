using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.DTOs.Create;

public class CreateBookingDto
{
    public string BookingReference { get; set; } = null!;

    public string PassengerId { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public decimal Price { get; set; }
    
    public static Booking ToBooking(CreateBookingDto dto)
    {
        return new Booking
        {
            Id = Guid.NewGuid().ToString(),
            BookingReference = dto.BookingReference,
            FlightId = dto.FlightId,
            PassengerId = dto.PassengerId,
        };
    }
    
    public static CreateBookingDto FromBooking(Booking booking)
    {
        return new CreateBookingDto
        {
            BookingReference = booking.BookingReference,
            FlightId = booking.FlightId,
            PassengerId = booking.PassengerId,
        };
    }


}