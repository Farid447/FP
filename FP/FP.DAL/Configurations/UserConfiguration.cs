﻿using FP.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FP.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.FIN);
        builder.HasIndex(x => x.UserName);

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(x=>x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(32);

        builder.Property(x => x.FIN)
            .IsRequired()
            .HasMaxLength(7);
    }
}
