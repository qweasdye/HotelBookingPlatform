using HotelBookingPlatform.Core.Domain.Entities;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> GetByIdAsync(int id);
        Task<bool> IsRoomExistsAsync(int id);
    }
}