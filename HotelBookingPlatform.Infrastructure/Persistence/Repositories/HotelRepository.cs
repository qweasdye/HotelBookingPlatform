using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;

        public HotelRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateHotelAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await GetHotelByIdAsync(id);

            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(r => r.Rooms)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Hotel>> SearchHotelsAsync(string query)
        {
            return await _context.Hotels
                .Where(h => h.Name.Contains(query) || h.Description.Contains(query))
                .ToListAsync();
        }

    }
}
