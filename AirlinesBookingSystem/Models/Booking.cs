using System;
using System.Collections.Generic;

namespace AirlinesBookingSystem.Models;

public partial class Booking
{
    public string Id { get; set; } = null!;

    public string BookingReference { get; set; } = null!;

    public string PassengerId { get; set; } = null!;

    public string FlightId { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;
}
