using MediatR;
using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using HotelBookingPlatform.Infrastructure.Persistence.Repositories;

namespace HotelBookingPlatform.Application.Features.Hotels.Commands
{
    public class CreateHotel : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public List<Room> Rooms { get; set; } = new();
    }

    public class CreateHotelHandler : IRequestHandler<CreateHotel, int>
    {
        private readonly HotelRepository _hotelRepository;

        public CreateHotelHandler(HotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public async Task<int> Handle(CreateHotel request, CancellationToken cancellationToken)
        {
            var hotel = new Hotel
            {
                Name = request.Name,
                Description = request.Description,
                Address = request.Address,
                Rooms = request.Rooms
            };

            await _hotelRepository.AddHotelAsync(hotel);
            return hotel.Id;
        }
    }
}
