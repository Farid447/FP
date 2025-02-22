using FP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.DAL.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rate)
            .IsRequired()
            .HasMaxLength(5);
        
        builder.Property(x => x.Feedback)
            .HasMaxLength(256);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Stadium)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.StadiumId);
    }
}
