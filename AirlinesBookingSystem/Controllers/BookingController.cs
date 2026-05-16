using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Events;
using AirlinesBookingSystem.Interfaces;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;


[ApiController]
[Route("[controller]")]
public class BookingController(IBookingService service, IAirlineClient client) : ControllerBase
{
    
    [HttpGet]
    [Route("/Get-All-Bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        var allBookings = await service.GetAllBookings();

        return Ok(allBookings);

    }
    
    /*[HttpGet]
    [Route("/Get-Booking-By-Id")]
    public async Task<IActionResult> GetBookingById([FromQuery] string bookingId)
    {
        var booking = await service.GetBookingById(bookingId);

        return Ok(booking);
        
    }*/
    
    [HttpPost]
    [Route("/Add-Booking")]
    public async Task<IActionResult> AddBooking(CreateBookingDto booking)
    {
        var (success, message) = await service.InitiateBookingAsync(booking);

        if (!success)
            return Conflict(message);

        return Ok();
    }
    
    [HttpPut]
    [Route("/Update-Booking")]
    public async Task<IActionResult> UpdateBooking(Booking booking)
    {
        await service.UpdateBooking(booking);
        return Ok();
    }
    
    /*[HttpDelete]
    [Route("/Delete-Booking")]
    public async Task<IActionResult> DeleteBooking(string bookingId)
    {
        await service.DeleteBooking(bookingId);
        return Ok();
    }*/
    
}