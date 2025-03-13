using Marten.Events.Aggregation;

namespace EventSourcingMarten.Controllers;

public class OrderProjection : SingleStreamProjection<Order>
{
    public void Apply(Events.OrderCreated @event, Order order)
    {
        order.Id = @event.Id;
        order.ProductName = @event.ProductName;
        order.DeliveryAddress = @event.DeliveryAddress;
    }

    public void Apply(Events.OrderAddressUpdated @event, Order order)
    {
        order.DeliveryAddress = @event.DeliveryAddress;
    }

    public void Apply(Events.OrderDispatched @event, Order order)
    {
        order.DispatchedAtUtc = @event.DispatchedAtUtc;
    }

    public void Apply(Events.OrderOutForDelivery @event, Order order)
    {
        order.OutForDeliveryAtUtc = @event.OutForDeliveryAtUtc;
    }

    public void Apply(Events.OrderDelivered @event, Order order)
    {
        order.DeliveredAtUtc = @event.DeliveredAtUtc;
    }


}
