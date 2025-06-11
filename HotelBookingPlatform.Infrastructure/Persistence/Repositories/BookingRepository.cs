using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;

        public BookingRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddBookingAsync(Booking booking) 
        {
            // Проверка на пересечение дат
            if (await IsRoomAlreadyBookedAsync(booking.RoomId, booking.CheckIn, booking.CheckOut))
            {
                throw new InvalidOperationException("Номер уже забронирован на указанные даты");
            }

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await GetBookingByIdAsync(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Booking>> GetAllBookingsAsync(string query)
        {
            var bookingsQuery = _context.Bookings
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                bookingsQuery = bookingsQuery.Where(b =>
                    b.Hotel.Name.Contains(query) ||
                    b.Room.RoomNumber.Contains(query) ||
                    b.GuestEmail.Contains(query));
            }

            return await bookingsQuery.ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(h => h.Hotel)
                .Include(r => r.Room)
                .Include(m => m.TotalPrice)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<BookingResult> GetBookingsForRoomAsync(int roomId, DateTime? checkIn = null, DateTime? checkOut = null)
        {
            var bookingsQueryRoom = await _context.Bookings
                .Include(h => h.Hotel)
                .Where(b => b.RoomId == roomId)
                .OrderBy(b => b.CheckIn)
                .ToListAsync();

            if (!checkIn.HasValue || !checkOut.HasValue)
            {
                return new BookingResult(false, "Все брони данного номера", bookingsQueryRoom);
            }

            var conflictingBookings = bookingsQueryRoom
                .Where(b => checkIn.Value < b.CheckOut && checkOut.Value > b.CheckIn)
                .ToList();

            return conflictingBookings.Count > 0
                ? new BookingResult(true, "Номер занят на указанные даты", conflictingBookings)
                : new BookingResult(false, "Номер свободен", new List<Booking>());
        }

        public async Task<bool> IsRoomAlreadyBookedAsync(int roomId, DateTime newCheckIn, DateTime newCheckOut)
        {
            // Получаем все брони для этого номера
            var existingBookings = await _context.Bookings
                .Where(b => b.RoomId == roomId)
                .ToListAsync();

            // Проверяем, есть ли пересечение дат
            bool isBooked = existingBookings.Any(b =>
                (newCheckIn < b.CheckOut && newCheckOut > b.CheckIn));

            return isBooked;
        }
    }
}
