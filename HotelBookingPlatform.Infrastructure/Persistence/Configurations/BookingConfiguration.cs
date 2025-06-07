using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingPlatform.Core.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            // Primary Key
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .UseIdentityAlwaysColumn()
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(b => b.GuestName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.GuestEmail)
                .IsRequired()
                .HasMaxLength(255);

            // Owned Type for Money
            builder.OwnsOne(b => b.TotalPrice, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("TotalPriceAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("TotalPriceCurrency")
                    .HasMaxLength(3)
                    .HasDefaultValue("USD")
                    .IsRequired();
            });

            // Relationships
            builder.HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Indexes
            builder.HasIndex(b => b.GuestEmail);
            builder.HasIndex(b => b.RoomId);
        }
    }
}