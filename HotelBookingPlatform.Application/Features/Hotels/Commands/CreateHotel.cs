﻿using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using HotelBookingPlatform.Infrastructure.Persistence;
using MediatR;

namespace HotelBookingPlatform.Core.Hotels.Commands
{
    public class CreateHotel : IRequest<int> // Возвращает ID созданного отеля
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; } // Вложенный DTO для адреса

        public class AddressDto
        {
            public string Street { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }
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
                    Rooms = new List<Room>() // Инициализируем пустой список комнат
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