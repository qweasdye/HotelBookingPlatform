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

        public async Task AddHotelAsync(Hotel hotel) // POST
        {
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateHotelAsync(Hotel hotel) // PUT
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int id) // DELETE
        {
            var hotel = await GetHotelByIdAsync(id);

            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id) // GET by id
        {
            return await _context.Hotels
                .Include(r => r.Rooms)
                .Include(a => a.Address)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Hotel>> SearchHotelsAsync(string query) // GET
        {
            var hotelsQuery = _context.Hotels
                .Include(h => h.Address)  // Добавляем подгрузку адреса
                .Include(h => h.Rooms) // Добавляем подгрузку номеров
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                hotelsQuery = hotelsQuery.Where(h =>
                    h.Name.Contains(query) ||
                    (h.Description != null && h.Description.Contains(query)));
            }

            return await hotelsQuery.ToListAsync();
        }

    }
}
