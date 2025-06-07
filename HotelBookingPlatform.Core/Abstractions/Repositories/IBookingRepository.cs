using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetByIdAsync(int id);
        Task<List<Booking>> GetBookingsForRoomAsync(int roomId);
        Task AddAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}
