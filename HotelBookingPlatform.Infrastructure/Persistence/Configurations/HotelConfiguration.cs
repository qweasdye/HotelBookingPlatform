using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingPlatform.Core.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            // Настройка первичного ключа (автоинкремент)
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id)
                  .UseIdentityAlwaysColumn()  // Для PostgreSQL (для SQL Server используйте .UseIdentityColumn())
                  .ValueGeneratedOnAdd();

            // Настройка обязательных свойств
            builder.Property(h => h.Name)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("Name");  // Используем стандартное имя столбца

            builder.Property(h => h.Description)
                  .HasMaxLength(1000);

            builder.HasOne(h => h.Address)
               .WithOne(a => a.Hotel)
               .HasForeignKey<Address>(a => a.HotelId);

            builder.HasMany(h => h.Rooms)
                   .WithOne(r => r.Hotel)
                   .HasForeignKey(r => r.HotelId);

            // Связь 1-to-1 с Address (каскадное удаление)
            builder.HasOne(h => h.Address)
                  .WithOne(a => a.Hotel)
                  .HasForeignKey<Address>(a => a.HotelId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Связь 1-to-Many с Room (каскадное удаление)
            builder.HasMany(h => h.Rooms)
                  .WithOne(r => r.Hotel)
                  .HasForeignKey(r => r.HotelId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}