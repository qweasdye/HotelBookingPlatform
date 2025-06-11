using HotelBookingPlatform.Core.Abstractions.Repositories;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Commands
{
    public class DeleteBooking : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteBookingHandler : IRequestHandler<DeleteBooking>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Task Handle(DeleteBooking request, CancellationToken cancellationToken)
        {
            
        }
    }
}
