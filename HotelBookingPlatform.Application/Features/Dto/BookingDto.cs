namespace HotelBookingPlatform.Application.Features.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public MoneyDto TotalPrice { get; set; }
        public HotelDto Hotel { get; set; }
        public RoomDto Room { get; set; }
    }
}
