using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController (IFlightService service ) : ControllerBase
{
    
    [HttpGet]
    [Route("/Get-All-Flights")]
    public async Task<IActionResult> GetAllFlights()
    {
        var allFlights = await service.GetAllFlights();

        return Ok(allFlights);

    }
    
    /*[HttpGet]
    [Route("/Get-Flight-By-Id")]
    public async Task<IActionResult> GetFlightById([FromQuery] string flightId)
    {
        var flight = await service.GetFlightById(flightId);

        return Ok(flight);
        
    }*/
    
    [HttpPost]
    [Route("/Add-Flight")]
    public async Task<IActionResult> AddFlight(CreateFlightDto flight)
    {
        await service.AddFlight(flight);
        return Ok();
    }
    
    [HttpPut]
    [Route("/Update-Flight")]
    public async Task<IActionResult> UpdateFlight(Flight flight)
    {
        await service.UpdateFlight(flight);
        return Ok();
    }
    
    /*[HttpDelete]
    [Route("/Delete-Flight")]
    public async Task<IActionResult> DeleteFlight(string flightId)
    {
        await service.DeleteFlight(flightId);
        return Ok();
    }*/
}