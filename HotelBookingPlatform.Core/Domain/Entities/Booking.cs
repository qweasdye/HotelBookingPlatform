﻿using HotelBookingPlatform.Core.Domain.ValueObjects;

namespace HotelBookingPlatform.Core.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail {  get; set; }
        public Money TotalPrice { get; set; }
    }
}
