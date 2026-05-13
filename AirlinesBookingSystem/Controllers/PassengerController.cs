using AirlinesBookingSystem.Interfaces.Services;
using AirlinesBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlinesBookingSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengerController(IPassengerService service) : ControllerBase
{
    [HttpGet]
    [Route("/Get-All")]
    public async Task<IActionResult> GetAllPassengers()
    {
        var allFlights = await service.GetAllPassengers();

        return Ok(allFlights);

    }
    
    /*[HttpGet]
    [Route("/Get-Id")]
    public async Task<IActionResult> GetPassengerById([FromQuery] string passengerId)
    {
        var flight = await service.GetPassengerById(passengerId);

        return Ok(flight);
        
    }*/
    
    [HttpPost]
    [Route("/Add")]
    public async Task<IActionResult> AddPassenger(Passenger passenger)
    {
        await service.AddPassenger(passenger);
        return Ok();
    }
    
    [HttpPut]
    [Route("/Update")]
    public async Task<IActionResult> UpdatePassenger(Passenger passenger)
    {
        await service.UpdatePassenger(passenger);
        return Ok();
    }
    
    /*[HttpDelete]
    [Route("/Delete")]
    public async Task<IActionResult> DeletePassenger(string passengerId)
    {
        await service.DeletePassenger(passengerId);
        return Ok();
    }*/
}