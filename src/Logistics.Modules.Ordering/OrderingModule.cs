using Logistics.Modules.Ordering.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Logistics.Modules.Ordering.Features.PlaceOrder;

namespace Logistics.Modules.Ordering;

public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");

        services.AddDbContext<OrderingDbContext>(options =>
            options.UseNpgsql(connectionString, x => 
                x.MigrationsHistoryTable("__EFMigrationsHistory", "ordering")));

        // MediatR needs to know to scan this specific assembly for Handlers
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(OrderingModule).Assembly));

        return services;
    }
    
    public static void MapOrderingEndpoints(this IEndpointRouteBuilder app)
    {
        // Register each feature's endpoint here
        PlaceOrderEndpoint.MapEndpoint(app);
    }
}