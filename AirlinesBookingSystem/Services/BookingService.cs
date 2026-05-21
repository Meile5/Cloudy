using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Repositories;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Shared.Events;

namespace AirlinesBookingSystem.Services;

public class BookingService(IBookingRepository repo, IAirlineClient client, ISeatLockService seatLockService, ISeatRepository seatRepo ) : IBookingService
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

        //once booking is made, seat is no longer available
        //so we also update mongodb
        var command = new MongoRemoveSeatCommand()
        {
            flightId = booking.FlightId,
            seatId = booking.SeatId,
        };
        await client.Publish<MongoRemoveSeatCommand>(command);
    }

    public async Task UpdateBooking(Booking booking)
    {
        await repo.UpdateBooking(booking);
    }

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
        });

        return (true, null);
    }
}