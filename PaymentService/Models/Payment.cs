using System;
using System.Collections.Generic;

namespace PaymentService.Models;

public partial class Payment
{
    public string Id { get; set; } = null!;

    public string CardNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public string Currency { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
