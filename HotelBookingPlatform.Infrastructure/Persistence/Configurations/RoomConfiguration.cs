using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingPlatform.Core.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");

            // Primary Key
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .UseIdentityAlwaysColumn()
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(r => r.RoomNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Capacity)
                .IsRequired();

            // Owned Type for Money
            builder.OwnsOne(r => r.PricePerNight, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("PricePerNightAmount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("PricePerNightCurrency")
                    .HasMaxLength(3)
                    .HasDefaultValue("USD")
                    .IsRequired();
            });

            // Relationships
            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(r => new { r.HotelId, r.RoomNumber })
                .IsUnique();
        }
    }
}