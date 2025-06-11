using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;

namespace HotelBookingPlatform.Application.Features.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public Money TotalPrice { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
