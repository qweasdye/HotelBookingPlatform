using HotelBookingPlatform.Core.Domain.Enums;
using HotelBookingPlatform.Core.Domain.ValueObjects;

namespace HotelBookingPlatform.Application.Features.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public Money PricePerNight { get; set; }
        public int Capacity { get; set; }
    }
}
