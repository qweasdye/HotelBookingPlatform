using HotelBookingPlatform.Core.Data.Configurations;
using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingPlatform.Infrastructure.Persistence
{
    public class HotelBookingDbContext : DbContext
    {
        public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options) { }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
