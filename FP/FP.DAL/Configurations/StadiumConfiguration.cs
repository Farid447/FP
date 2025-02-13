using FP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.DAL.Configurations;

public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
{
    public void Configure(EntityTypeBuilder<Stadium> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.LocationLink)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x=>x.Description)
            .HasMaxLength(128);

        builder.Property(x=>x.StadiumCount)
            .HasMaxLength(5);

        builder.Property(x => x.RoomCount)
            .HasMaxLength(20);
    }
}
