using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Queries
{
    public class GetAllBookings : IRequest<List<Booking>>
    {
        public string? SearchQuery { get; }

        public GetAllBookings(string? searchQuery = null)
        {
            SearchQuery = searchQuery;
        }
    }

    public class GetAllBookingsHandler : IRequestHandler<GetAllBookings, List<Booking>>
    {
        private readonly IBookingRepository _bookingRepository;
        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> Handle(GetAllBookings request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetAllBookingsAsync(request.SearchQuery);
        }
    }
}
