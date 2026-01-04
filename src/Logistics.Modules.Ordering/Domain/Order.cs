namespace Logistics.Modules.Ordering.Domain;

public class Order
{
    // EF Core will now find this Primary Key
    public Guid Id { get; private set; } 
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }

    // Encapsulation: Use a private field for items so they can't be modified 
    // from outside without using the AddItem method
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    // Required by EF Core
    private Order() { }

    public static Order Create(Guid customerId)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending
        };
    }

    public void AddItem(Guid productId, int quantity, decimal price)
    {
        // TODO: Discuss adding guard clauses (ardalis guard clauses? https://github.com/ardalis/GuardClauses)
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive");
        
        _items.Add(new OrderItem(Id, productId, quantity, price));
    }
}

public enum OrderStatus { Pending, Paid, Shipped, Canceled }