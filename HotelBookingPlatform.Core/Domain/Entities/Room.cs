using HotelBookingPlatform.Core.Domain.ValueObjects;

namespace HotelBookingPlatform.Core.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string RoomNumber {  get; set; }
        public string Type { get; set; }
        public Money PricePerNight { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
