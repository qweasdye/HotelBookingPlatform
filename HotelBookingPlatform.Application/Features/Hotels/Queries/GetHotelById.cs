using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Queries
{
    public class GetHotelById : IRequest<Hotel>
    {
        public int Id { get; set; }
    }

    public class GetHotelByIdHandler : IRequestHandler<GetHotelById, Hotel>
    {
        private readonly IHotelRepository _hotelRepository;

        public GetHotelByIdHandler(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Hotel> Handle(GetHotelById request, CancellationToken cancellationToken)
        {
            return await _hotelRepository.GetHotelByIdAsync(request.Id);
        }
    }
}