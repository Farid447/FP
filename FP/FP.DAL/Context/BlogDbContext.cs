using FP.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FP.DAL.Context;

public class FPDbContext : IdentityDbContext<User>
{
    public DbSet<Stadium> Stadiums { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public FPDbContext(DbContextOptions opt) : base(opt)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FPDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
