using HotelBookingPlatform.Application.Features.Dto;
using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using MediatR;

namespace HotelBookingPlatform.Application.Features.Bookings.Commands
{
    public class CreateBooking : IRequest<BookingDto>
    {
        public int RoomId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    public class CreateBookingHandler : IRequestHandler<CreateBooking, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateBookingHandler(
            IBookingRepository bookingRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<BookingDto> Handle(CreateBooking request, CancellationToken cancellationToken)
        {
            // Получаем данные о номере
            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new InvalidOperationException("Номер не найден");

            // Проверяем доступность дат 
            if (await _bookingRepository.IsRoomAlreadyBookedAsync(
                request.RoomId, request.CheckIn, request.CheckOut))
            {
                throw new InvalidOperationException("Номер занят на выбранные даты");
            }

            // Создаем объект бронирования
            var booking = new Booking
            {
                RoomId = request.RoomId,
                Room = room,
                GuestName = request.GuestName,
                GuestEmail = request.GuestEmail,
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut,
                TotalPrice = CalculateTotalPrice(room.PricePerNight, request.CheckIn, request.CheckOut),
                HotelId = room.HotelId,
                Hotel = room.Hotel
            };

            var createdBooking = await _bookingRepository.AddBookingAsync(booking);

            return MapToDto(createdBooking);
        }

        private Money CalculateTotalPrice(Money pricePerNight, DateTime checkIn, DateTime checkOut)
        {
            var nights = (checkOut - checkIn).Days;
            return new Money
            {
                Amount = pricePerNight.Amount * nights,
                Currency = pricePerNight.Currency
            };
        }

        private BookingDto MapToDto(Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                GuestName = booking.GuestName,
                GuestEmail = booking.GuestEmail,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                TotalPrice = new MoneyDto
                {
                    Amount = booking.TotalPrice.Amount,
                    Currency = booking.TotalPrice.Currency
                },
                Hotel = new HotelDto
                {
                    Id = booking.Hotel.Id,
                    Name = booking.Hotel.Name,
                    Description = booking.Hotel.Description
                },
                Room = new RoomDto
                {
                    Id = booking.Room.Id,
                    RoomNumber = booking.Room.RoomNumber,
                    Type = booking.Room.Type,
                    Capacity = booking.Room.Capacity
                }
            };
        }
    }
}
