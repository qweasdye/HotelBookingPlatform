using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;

        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.PricePerNight)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> IsRoomExistsAsync(int id)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == id);
        }
    }
}