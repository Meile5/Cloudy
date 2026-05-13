using System;
using System.Collections.Generic;

namespace AirlinesBookingSystem.Models;

public partial class Flight
{
    public string Id { get; set; } = null!;

    public string FlightNumber { get; set; } = null!;

    public string OriginAirport { get; set; } = null!;

    public string DestinationAirport { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string? AircraftId { get; set; }

    public string? Status { get; set; }

    public decimal BaseFare { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
