using HotelBookingPlatform.Core.Abstractions.Repositories;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class UpdateHotel : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateHotelHandler : IRequestHandler<UpdateHotel>
    {
        private readonly IHotelRepository _hotelRepository;

        public UpdateHotelHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task Handle(UpdateHotel request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.Id);
            if (hotel == null) throw new Exception("Hotel not found");

            hotel.Name = request.Name;
            hotel.Description = request.Description;

            await _hotelRepository.UpdateHotelAsync(hotel);
        }
    }
}