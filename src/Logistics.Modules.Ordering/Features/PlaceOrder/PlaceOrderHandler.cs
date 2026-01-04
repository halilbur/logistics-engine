using Logistics.Modules.Ordering.Domain;
using Logistics.Modules.Ordering.Infrastructure;
using Logistics.Shared.Abstractions;
using MediatR;

namespace Logistics.Modules.Ordering.Features.PlaceOrder;

public record PlaceOrderCommand(Guid CustomerId, List<OrderItemDto> Items) : IRequest<Guid>;
public record OrderItemDto(Guid ProductId, int Quantity, decimal Price);
public class PlaceOrderHandler(OrderingDbContext db) 
    : IRequestHandler<PlaceOrderCommand, Guid>, IScopedService 
{
    public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        var order = Order.Create(request.CustomerId);
        
        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.Quantity, item.Price);
        }
        
        db.Orders.Add(order);
        await db.SaveChangesAsync(ct);

        return order.Id;
    }
}