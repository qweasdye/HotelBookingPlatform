using HotelBookingPlatform.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingPlatform.Core.Domain.ValueObjects
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
