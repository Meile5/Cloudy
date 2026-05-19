using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Interfaces.Services;
using PaymentService.Models;

namespace PaymentService.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IPaymentService service) : ControllerBase
{
    [HttpGet]
    [Route("/Get-All-Payments")]
    public async Task<IActionResult> GetAllPayments()
    {
        var allPayments = await service.GetAllPayments();
        return Ok(allPayments);
    }

    [HttpPost]
    [Route("/Add-Payment")]
    public async Task<IActionResult> AddPayment(CreatePaymentDto payment)
    {
        await service.AddPayment(payment);
        return Ok();
    }

    [HttpPut]
    [Route("/Update-Payment")]
    public async Task<IActionResult> UpdatePayment(Payment payment)
    {
        await service.UpdatePayment(payment);
        return Ok();
    }
}