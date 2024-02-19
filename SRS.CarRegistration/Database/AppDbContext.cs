using Microsoft.EntityFrameworkCore;
using SRS.CarRegistration.Models;

namespace SRS.CarRegistration.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>()
            .HasMany(e => e.Cars)
            .WithMany(e => e.Owners)
            .UsingEntity(
                "CarOwner",
                l => l.HasOne(typeof(Car)).WithMany().HasForeignKey("CarID").HasPrincipalKey(nameof(Car.CarID)),
                r => r.HasOne(typeof(Owner)).WithMany().HasForeignKey("OwnerID").HasPrincipalKey(nameof(Owner.OwnerID)),
                j => j.HasKey("OwnerID", "CarID"));
    }
}
