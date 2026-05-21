namespace AirlinesBookingSystem.DTOs.Create;

public class CreatePaymentDto
{
    public string CardNumber { get; set; } = null!;

    public string Currency { get; set; } = null!;
}