using HotelBookingPlatform.Core.Abstractions.Repositories;
using MediatR;
using OpenQA.Selenium;

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
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.Id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id {request.Id} not found");
            }

            await _hotelRepository.DeleteHotelAsync(request.Id);
        }
    }
}