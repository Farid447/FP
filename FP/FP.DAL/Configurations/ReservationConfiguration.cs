using FP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.DAL.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=>x.RoomNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.PitchNumber)
            .IsRequired()
            .HasMaxLength(5);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Stadium)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.StadiumId);
    }
}
