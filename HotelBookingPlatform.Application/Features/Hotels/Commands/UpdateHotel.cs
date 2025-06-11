using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using HotelBookingPlatform.Application.Features.Dto;
using OpenQA.Selenium;
using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class UpdateHotel : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        required public AddressDto Address { get; set; }
        public RoomDto Rooms { get; set; }
    }

    public class UpdateHotelHandler : IRequestHandler<UpdateHotel, Unit>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly ILogger<UpdateHotelHandler> _logger;

        public UpdateHotelHandler(
            IHotelRepository hotelRepository,
            ILogger<UpdateHotelHandler> logger)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateHotel request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.Id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with ID {request.Id} not found");
            }

            // Обновляем основные свойства
            hotel.Name = request.Name ?? hotel.Name;
            hotel.Description = request.Description ?? hotel.Description;

            // Обновляем адрес
            if (request.Address != null)
            {
                hotel.Address ??= new Address();
                hotel.Address.Street = request.Address.Street ?? hotel.Address.Street;
                hotel.Address.City = request.Address.City ?? hotel.Address.City;
                hotel.Address.Country = request.Address.Country ?? hotel.Address.Country;
            }

            // Добавляем новые комнаты
            if (request.Rooms != null)
            {
                var newRoom = new Room
                {
                    RoomNumber = request.Rooms.RoomNumber,
                    Type = request.Rooms.Type,
                    PricePerNight = request.Rooms.PricePerNight,
                    Capacity = request.Rooms.Capacity,
                    HotelId = hotel.Id
                };
                hotel.Rooms.Add(newRoom);
            }

            await _hotelRepository.UpdateHotelAsync(hotel);
            return Unit.Value;
        }
    }
}
