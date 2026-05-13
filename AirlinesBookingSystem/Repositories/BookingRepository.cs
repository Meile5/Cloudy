using AirlinesBookingSystem.Database;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesBookingSystem.Repositories;

public class BookingRepository(BookingContext context) : IBookingRepository
{

    public async Task<List<Booking>> GetAllBookings()
    {
        return await context.Bookings.ToListAsync();
    }
    
    public async Task<Booking> GetBookingById(string bookingId)
    {
        return await context.Bookings.Where(b => b.Id == bookingId).FirstOrDefaultAsync();
    }
    
    public async Task AddBooking(Booking booking)
    {
        context.Bookings.Add(booking);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateBooking(Booking booking)
    {
       context.Bookings.Update(booking);
       await context.SaveChangesAsync();
    }
    
    //we prob dont want to hard delete a booking, but I'll but this here in case
    public async Task DeleteBooking(string bookingId)
    {
        var toDelete = await context.Bookings.Where(b => b.Id == bookingId).FirstOrDefaultAsync();
        context.Bookings.Remove(toDelete);
        await context.SaveChangesAsync();
    }
    
}