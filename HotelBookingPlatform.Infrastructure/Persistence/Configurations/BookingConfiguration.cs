using HotelBookingPlatform.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        // Первичный ключ
        builder.HasKey(b => b.Id);

        // Настройка связей
        builder.HasOne(b => b.Room)
               .WithMany()
               .HasForeignKey(b => b.RoomId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Hotel)
               .WithMany()
               .HasForeignKey(b => b.HotelId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(b => b.TotalPrice, money =>
        {
            money.Property(m => m.Amount).HasColumnName("TotalAmount");
            money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3);
        });

        // Настройка свойств
        builder.Property(b => b.GuestName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.GuestEmail)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.CheckIn)
               .IsRequired();

        builder.Property(b => b.CheckOut)
               .IsRequired();

        // Индексы
        builder.HasIndex(b => b.RoomId);
        builder.HasIndex(b => b.HotelId);
        builder.HasIndex(b => b.GuestEmail);
    }
}