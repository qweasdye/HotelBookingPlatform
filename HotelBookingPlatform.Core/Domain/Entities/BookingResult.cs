namespace HotelBookingPlatform.Core.Domain.Entities
{
    public class BookingResult
    {
        public bool IsBooked { get; set; }
        public string Message { get; set; }
        public List<Booking> Bookings { get; set; }
        public BookingResult(bool isBooked, string message, List<Booking> bookings)
        {
            IsBooked = isBooked;
            Message = message;
            Bookings = bookings ?? new List<Booking>();
        }
    }
}
