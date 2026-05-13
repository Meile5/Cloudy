using AirlinesBookingSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace AirlinesBookingSystem.DTOs.Create;

public class CreatePassengerDto
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? PassportNumber { get; set; }

    public string? FrequentFlyerNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    
    public static Passenger ToSeat(CreatePassengerDto dto)
    {
        return new Passenger
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone.IsNullOrEmpty() ? "" : dto.Phone,
            DateOfBirth = dto.DateOfBirth,
            PassportNumber = dto.PassportNumber.IsNullOrEmpty() ? "" : dto.PassportNumber,
            FrequentFlyerNumber = dto.FrequentFlyerNumber,
            CreatedAt = dto.CreatedAt == null ? DateTime.Now : dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt == null ? DateTime.Now : dto.UpdatedAt,
        };
    }
    
    public static CreatePassengerDto FromPassenger(Passenger pass)
    {
        return new CreatePassengerDto
        {
            Id = pass.Id,
            FirstName = pass.FirstName,
            LastName = pass.LastName,
            Email = pass.Email,
            Phone = pass.Phone,
            DateOfBirth = pass.DateOfBirth,
            PassportNumber = pass.PassportNumber,
            FrequentFlyerNumber = pass.FrequentFlyerNumber,
            CreatedAt = pass.CreatedAt,
            UpdatedAt = pass.UpdatedAt
        };
    }
    
}