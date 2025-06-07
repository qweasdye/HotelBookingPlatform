using HotelBookingPlatform.Core.Domain.Entities;
using HotelBookingPlatform.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelBookingPlatform.Core.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            // Primary Key
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .UseIdentityAlwaysColumn()
                .ValueGeneratedOnAdd();

            // Properties
            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(a => a.Hotel)
                .WithOne(h => h.Address)
                .HasForeignKey<Address>(a => a.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(a => a.HotelId)
                .IsUnique();
        }
    }
}