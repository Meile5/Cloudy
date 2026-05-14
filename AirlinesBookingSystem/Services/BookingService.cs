using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;

namespace AirlinesBookingSystem.Services;

public class BookingService(IBookingRepository repo) : IBookingService
{
    public async Task<List<Booking>> GetAllBookings()
    {
        return await repo.GetAllBookings();
    }
    
    public async Task<Booking> GetBookingById(string bookingId)
    {
        try
        {
            return await repo.GetBookingById(bookingId);
        }
        catch (Exception e)
        {
            Console.WriteLine("probably couldn't find booking with that id :(");
            Console.WriteLine(e.StackTrace);
            throw;
        }
        
    }
    
    public async Task AddBooking(CreateBookingDto booking)
    {
        var newBooking = CreateBookingDto.ToBooking(booking);
        await repo.AddBooking(newBooking);
    }
    
    public async Task UpdateBooking(Booking booking)
    {
        await repo.UpdateBooking(booking);
    }
    
    //we prob dont want to hard delete a booking, but I'll but this here in case
    public async Task DeleteBooking(string bookingId)
    {
        await repo.DeleteBooking(bookingId);
    }
}