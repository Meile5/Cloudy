using Microsoft.AspNetCore.Mvc;
using MongoReadModel.MongoDb.Interfaces;

namespace MongoReadModel.MongoDb.Controllers;


[ApiController]
[Route("[controller]")]
public class MongoFlightController(IMongoFlightService service) : ControllerBase
{
    
    [HttpGet]
    [Route("/Get-Mongo-Flights")]
    public async Task<IActionResult> GetAllFlights()
    {
        var allFlights = await service.GetAllFlights();

        return Ok(allFlights);

    }
    
    
}