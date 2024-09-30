using BillingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BillingSystem.Data
{public class BillingContext : IdentityDbContext<User>
{
    public BillingContext(DbContextOptions<BillingContext> options)
        : base(options)
    {
    }

    public DbSet<Bill> Bills { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Tariff> Tariffs { get; set; }
    
    //public DbSet<User> Users { get; set; } // Optional, if using custom User

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Ensure base configuration is applied

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Service)
            .WithMany()
            .HasForeignKey(b => b.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Bill>()
            .HasOne(b => b.Tariff)
            .WithMany()
            .HasForeignKey(b => b.TariffId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
}