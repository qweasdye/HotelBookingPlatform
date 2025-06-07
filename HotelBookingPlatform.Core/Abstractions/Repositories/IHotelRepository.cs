using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetByIdAsync(int id);
        Task<List<Hotel>> SearchHotelsAsync(string query);
        Task AddAsync(Hotel hotel);
        Task UpdateAsync(Hotel hotel);
        Task DeleteAsync(int id);
    }
}
