namespace HotelBookingPlatform.Application.Features.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }
}
