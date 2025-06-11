using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Queries
{
    public class GetBookingsForRoom : IRequest<BookingResult>
    {
        public int RoomId { get; }
        public DateTime? CheckIn { get; }
        public DateTime? CheckOut { get; }

        public GetBookingsForRoom(int roomId, DateTime? checkIn = null, DateTime? checkOut = null)
        {
            RoomId = roomId;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }
    }

    public class GetBookingsForRoomHandler : IRequestHandler<GetBookingsForRoom, BookingResult>
    {
        private readonly IBookingRepository _repositoryBooking;

        public GetBookingsForRoomHandler(IBookingRepository repositoryBooking)
        {
            _repositoryBooking = repositoryBooking;
        }

        public async Task<BookingResult> Handle(GetBookingsForRoom request, CancellationToken cancellationToken)
        {
            return await _repositoryBooking.GetBookingsForRoomAsync(
                request.RoomId,
                request.CheckIn,
                request.CheckOut);
        }
    }
}
