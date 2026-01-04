using Logistics.Modules.Shipping.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Shipping.Infrastructure;
public class ShippingDbContext(DbContextOptions<ShippingDbContext> options) : DbContext(options)
{
    public DbSet<Shipment> Orders => Set<Shipment>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("shipping");
        modelBuilder.Entity<Shipment>().ToTable("Shipments");
    }
}