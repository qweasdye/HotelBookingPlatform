using HotelBookingPlatform.Core.Abstractions.Repositories;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Commands
{
    public class DeleteBooking : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteBooking(int id)
        {
            Id = Id;
        }
    }

    public class DeleteBookingHandler : IRequestHandler<DeleteBooking, bool>
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> Handle(DeleteBooking request, CancellationToken cancellationToken)
        {
            await _bookingRepository.DeleteBookingAsync(request.Id);
            return true;
        }
    }
}
