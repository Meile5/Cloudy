using PaymentService.Models;

namespace PaymentService.DTOs;

public class CreatePaymentDto
{
    public string CardNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    //public string Status { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public static Payment toPayment(CreatePaymentDto dto)
    {
        return new Payment
        {
            Id = Guid.NewGuid().ToString(),
            CardNumber = dto.CardNumber,
            Amount = dto.Amount,
            Status = "reserved",
            Currency = dto.Currency,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
    
    public static CreatePaymentDto fromPayment(Payment dto)
    {
        return new CreatePaymentDto()
        {
            CardNumber = dto.CardNumber,
            Amount = dto.Amount,
            Currency = dto.Currency,
        };
    }
}