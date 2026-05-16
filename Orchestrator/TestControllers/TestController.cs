using Microsoft.AspNetCore.Mvc;
using Orchestrator.Interfaces;
using Shared.Events;

namespace Orchestrator.TestControllers;

//this is in case we want to try sending any events
[ApiController]
[Route("[controller]")]
public class TestController(IAirlineClient client) : ControllerBase
{
    
    [HttpPost]
    [Route("/Payment-Success")]
    public async Task<IActionResult> PaymentSuccess(PaymentSuccessEvent paymentEvent)
    {
        await client.Publish<PaymentSuccessEvent>(paymentEvent);
        return Ok();
    }
    
}