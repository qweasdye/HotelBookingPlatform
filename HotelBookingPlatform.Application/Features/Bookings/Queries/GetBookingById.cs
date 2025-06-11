using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Queries
{
    public class GetBookingById : IRequest<Booking?>
    {
        public int Id { get; }
        public GetBookingById(int id)
        {
            Id = id;
        }
    }

    public class GetBookingByIdHandler : IRequestHandler<GetBookingById, Booking?>
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking?> Handle(GetBookingById request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetBookingByIdAsync(request.Id);
        }
    }
}
