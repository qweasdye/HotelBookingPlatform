using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<List<Hotel>> SearchHotelsAsync(string query);
        Task AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int id);
    }
}
