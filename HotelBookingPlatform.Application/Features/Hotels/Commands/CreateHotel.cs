using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class CreateHotel : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
    }

    public class CreateHotelHandler : IRequestHandler<CreateHotel, int>
    {
        private readonly IHotelRepository _hotelRepository;

        public CreateHotelHandler(IHotelRepository hotelRepository)
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
                Rooms = new List<Room>()
            };

            await _hotelRepository.AddHotelAsync(hotel);


            return hotel.Id;
        }
    }
}
