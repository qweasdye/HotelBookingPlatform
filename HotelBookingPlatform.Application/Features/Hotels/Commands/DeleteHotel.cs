using HotelBookingPlatform.Core.Abstractions.Repositories;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class DeleteHotel : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteHotelHandler : IRequestHandler<DeleteHotel>
    {
        private readonly IHotelRepository _hotelRepository;

        public DeleteHotelHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task Handle(DeleteHotel request, CancellationToken cancellationToken)
        {
            await _hotelRepository.DeleteHotelAsync(request.Id);
        }
    }
}