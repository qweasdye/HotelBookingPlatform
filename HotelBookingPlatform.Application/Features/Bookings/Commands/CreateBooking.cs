using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Commands
{
    public class CreateBooking : IRequest<Booking>
    {
        public Booking Booking { get; set; }

        public CreateBooking(Booking booking)
        {
            Booking = booking;
        }
    }

    public class CreateBookingHandler : IRequestHandler<CreateBooking, Booking>
    {
        private readonly IBookingRepository _bookingRepository;
        public CreateBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task<Booking> Handle(CreateBooking request, CancellationToken cancellationToken)
        {
            return await _bookingRepository.AddBookingAsync(request.Booking);
        }
    }
}
