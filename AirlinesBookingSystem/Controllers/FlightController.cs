using AirlinesBookingSystem.DTOs.Create;
using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController (IFlightService service ) : ControllerBase
{
    //still useful to test if mongo and sql are in sync
    [HttpGet]
    [Route("/Get-All-Flights")]
    public async Task<IActionResult> GetAllFlights()
    {
        var allFlights = await service.GetAllFlights();

        return Ok(allFlights);

    }
    
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
    
    
}