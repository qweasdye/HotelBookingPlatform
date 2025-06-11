using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingPlatform.Application.Features.Dto
{
    public class BookingDto
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        [Required, MaxLength(100)]
        public string GuestName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string GuestEmail { get; set; }

        public MoneyDto TotalPrice { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }
    }
}
