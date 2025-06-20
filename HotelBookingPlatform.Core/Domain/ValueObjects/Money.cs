﻿using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Core.Domain.ValueObjects
{
    [Owned]
    public class Money
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
    }
}
