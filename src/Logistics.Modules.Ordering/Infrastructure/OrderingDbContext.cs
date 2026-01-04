using Logistics.Modules.Ordering.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.Ordering.Infrastructure;

public class OrderingDbContext(DbContextOptions<OrderingDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ordering");

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId);
        });

        modelBuilder.Entity<OrderItem>().HasKey(i => i.Id);
    }
}