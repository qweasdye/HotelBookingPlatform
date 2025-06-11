using HotelBookingPlatform.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingPlatform.Core.Abstractions.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task<BookingResult> GetBookingsForRoomAsync(int roomId, DateTime? checkIn = null, DateTime? checkOut = null);
        Task<List<Booking>> GetAllBookingsAsync(string query);
        Task<bool> IsRoomAlreadyBookedAsync(int roomId, DateTime newCheckIn, DateTime newCheckOut);
        Task<Booking> AddBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
    }
}
