using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Shared.Events;

namespace AirlinesBookingSystem.Services;

public class BookingService(IBookingRepository repo, IAirlineClient client, ISeatLockService seatLockService, ISeatService seatService ) : IBookingService
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

    public async Task<Booking> AddBooking(CreateBookingDto booking)
    {
        var newBooking = CreateBookingDto.ToBooking(booking);
        await repo.AddBooking(newBooking);

        await seatService.SellSeat(booking.SeatId);

        return newBooking;
    }

    public async Task UpdateBooking(Booking booking)
    {
        await repo.UpdateBooking(booking);
    }

    //we prob dont want to hard delete a booking, but I'll put this here in case
    public async Task DeleteBooking(string bookingId)
    {
        await repo.DeleteBooking(bookingId);
    }
    
    
    public async Task<(bool Success, string? Message)> InitiateBookingAsync(CreateBookingDto booking)
    {
        var sagaId = Guid.NewGuid();

        var locked = await seatLockService.TryLockSeatAsync(
            booking.FlightId,
            booking.SeatId,
            sagaId.ToString()
        );

        if (!locked)
            return (false, "Seat is currently held by another passenger.");

        await client.Publish(new BookingStartedEvent
        {
            SagaId = sagaId,
            BookingReference = booking.BookingReference,
            PassengerId = booking.PassengerId,
            FlightId = booking.FlightId,
            SeatId = booking.SeatId,
            Amount = booking.Price,
            CardNumber = booking.CardNumber!,
            Currency = booking.Currency!,
            
        });

        return (true, null);
    }
}