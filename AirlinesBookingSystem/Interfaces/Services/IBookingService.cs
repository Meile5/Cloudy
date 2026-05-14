using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Interfaces.Services;

public interface IBookingService
{
    public  Task<List<Booking>> GetAllBookings();

    public  Task<Booking> GetBookingById(string bookingId);

    public  Task AddBooking(CreateBookingDto booking);

    public Task UpdateBooking(Booking booking);

    public Task DeleteBooking(string bookingId);
}