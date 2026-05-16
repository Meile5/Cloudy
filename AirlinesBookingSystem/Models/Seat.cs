using System;
using System.Collections.Generic;

namespace AirlinesBookingSystem.Models;

public partial class Seat
{
    public string Id { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public string SeatNumber { get; set; } = null!;

    public string CabinClass { get; set; } = null!;

    public string? FareClass { get; set; }

    public string Status { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Flight Flight { get; set; } = null!;
}
