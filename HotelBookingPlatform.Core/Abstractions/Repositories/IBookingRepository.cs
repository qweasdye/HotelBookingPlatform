using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task<List<Booking>> GetBookingsForRoomAsync(int roomId);
        Task AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
    }
}
