using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class SeatController(ISeatService service) : ControllerBase
{
    [HttpGet]
    [Route("/Get-All")]
    public async Task<IActionResult> GetAllSeats()
    {
        var allFlights = await service.GetAllSeats();

        return Ok(allFlights);

    }
    
    /*[HttpGet]
    [Route("/Get-Id")]
    public async Task<IActionResult> GetSeatById([FromQuery] string seatId)
    {
        var flight = await service.GetSeatById(seatId);

        return Ok(flight);
        
    }*/
    
    [HttpPost]
    [Route("/Add")]
    public async Task<IActionResult> AddSeat(Seat seat)
    {
        await service.AddSeat(seat);
        return Ok();
    }
    
    [HttpPut]
    [Route("/Update")]
    public async Task<IActionResult> UpdateSeat(Seat seat)
    {
        await service.UpdateSeat(seat);
        return Ok();
    }
    
    /*[HttpDelete]
    [Route("/Delete")]
    public async Task<IActionResult> DeleteSeat(string seatId)
    {
        await service.DeleteSeat(seatId);
        return Ok();
    }*/
}