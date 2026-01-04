using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Logistics.Modules.Ordering.Features.PlaceOrder;

public static class PlaceOrderEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/orders", async (PlaceOrderCommand command, ISender sender) =>
            {
                var orderId = await sender.Send(command);
                return Results.Ok(orderId);
            })
            .WithTags("Orders")
            .WithName("PlaceOrder");
    }
}