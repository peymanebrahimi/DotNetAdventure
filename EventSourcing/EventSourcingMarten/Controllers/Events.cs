namespace EventSourcingMarten.Controllers;

public static class Events
{
    public class OrderCreated
    {
        public Guid Id { get; set; } = Guid.CreateVersion7();
        public string ProductName { get; set; }
        public string DeliveryAddress { get; set; }
    }

    public class OrderAddressUpdated
    {
        public Guid Id { get; set; } 
        public string DeliveryAddress { get; set; }
    }

    public class  OrderDispatched
    {
        public Guid Id { get; set; }
        public DateTime DispatchedAtUtc { get; set; }
    }

    public class OrderOutForDelivery
    {
        public Guid Id { get; set; }
        public DateTime OutForDeliveryAtUtc { get; set; }
    }

    public class OrderDelivered
    {
        public Guid Id { get; set; }
        public DateTime DeliveredAtUtc { get; set; }
    }
}

public class Order
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? DispatchedAtUtc { get; set; }
    public DateTime? OutForDeliveryAtUtc { get; set; }
    public DateTime? DeliveredAtUtc { get; set; }

    public void Apply(Events.OrderCreated @event)
    {
        ProductName = @event.ProductName;
        DeliveryAddress = @event.DeliveryAddress;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void Apply(Events.OrderAddressUpdated @event)
    {
        DeliveryAddress = @event.DeliveryAddress;
    }

    public void Apply(Events.OrderDispatched @event)
    {
        DispatchedAtUtc = @event.DispatchedAtUtc;
    }

    public void Apply(Events.OrderOutForDelivery @event)
    {
        OutForDeliveryAtUtc = @event.OutForDeliveryAtUtc;
    }

    public void Apply(Events.OrderDelivered @event)
    {
        DeliveredAtUtc = @event.DeliveredAtUtc;
    }
        
}
