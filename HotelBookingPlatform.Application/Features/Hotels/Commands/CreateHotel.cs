using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using HotelBookingPlatform.Infrastructure.Persistence;
using HotelBookingPlatform.Application.Features.Dto;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class CreateHotel : IRequest<int> // Возвращает ID созданного отеля
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto? Address { get; set; } // Вложенный DTO для адреса
        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>(); // Вложенный DTO для номеров
        public class CreateHotelHandler : IRequestHandler<CreateHotel, int>
        {
            private readonly HotelBookingDbContext _context;

            public CreateHotelHandler(HotelBookingDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(CreateHotel request, CancellationToken cancellationToken)
            {
                // Создаем новый отель
                var hotel = new Hotel
                {
                    Name = request.Name,
                    Description = request.Description,
                    Address = new Address
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        Country = request.Address.Country
                    },
                    Rooms = request.Rooms.Select(r => new Room
                    {
                        RoomNumber = r.RoomNumber,
                        Type = r.Type,
                        PricePerNight = r.PricePerNight,
                        Capacity = r.Capacity
                    }).ToList()
                };

                // Добавляем отель в контекст
                await _context.Hotels.AddAsync(hotel, cancellationToken);

                // Сохраняем изменения в БД
                await _context.SaveChangesAsync(cancellationToken);

                // Возвращаем ID созданного отеля
                return hotel.Id;
            }
        }
    }
}