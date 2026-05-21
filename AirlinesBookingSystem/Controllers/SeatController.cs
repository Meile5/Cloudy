using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.DTOs.Update;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class SeatController(ISeatService service) : ControllerBase
{
    [HttpGet]
    [Route("/Get-All-Seats")]
    public async Task<IActionResult> GetAllSeats()
    {
        var allFlights = await service.GetAllSeats();

        return Ok(allFlights);

    }
    

    [HttpPost]
    [Route("/Add-Seat")]
    public async Task<IActionResult> AddSeat([FromBody] CreateSeatDto seat)
    {
        await service.AddSeat(seat);
        return Ok();
    }
    
    [HttpPut]
    [Route("/Update-Seat")]
    public async Task<IActionResult> UpdateSeat([FromBody] UpdateSeatDto seat)
    {
        await service.UpdateSeat(seat);
        return Ok();
    }
    
    
}