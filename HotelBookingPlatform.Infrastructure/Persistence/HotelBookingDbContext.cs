using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Persistence
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base (options) {}

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений и индексов

            // Один отель имеет один адрес (1:1)
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Address)
                .WithOne(a => a.Hotel)
                .HasForeignKey<Address>(a => a.HotelId);

            // Один отель имеет много комнат (1:N)
            modelBuilder.Entity<Hotel>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId);

            // Уникальный номер комнаты в пределах отеля
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.HotelId, r.RoomNumber })
                .IsUnique();
        }
    }
}
