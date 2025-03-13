using Marten;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingMarten.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IDocumentStore store, IQuerySession session) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = session.Query<Order>().ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await session.LoadAsync<Order>(orderId);

            return order is not null ? Ok(order) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand command)
        {
            var order = new Events.OrderCreated
            {
                ProductName = command.ProductName,
                DeliveryAddress = command.DeliveryAddress
            };

            await using var session = store.LightweightSession();
            session.Events.StartStream<Order>(order.Id, order);
            await session.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPost("{orderId:guid}/address")]
        public async Task<IActionResult> DeliveryAddressUpdate(Guid orderId, [FromBody] DeliveryAddressUpdateCommand command)
        {
            var updatedAddress = new Events.OrderAddressUpdated
            {
                Id = orderId,
                DeliveryAddress = command.DeliveryAddress
            };
            await using var session = store.LightweightSession();
            session.Events.Append(updatedAddress.Id, updatedAddress);
            await session.SaveChangesAsync();
            return Ok(updatedAddress);
        }

        [HttpPost("{orderId:guid}/dispatch")]
        public async Task<IActionResult> DispatchOrder(Guid orderId)
        {
            var dispatched = new Events.OrderDispatched
            {
                Id = orderId,
                DispatchedAtUtc = DateTime.UtcNow
            };
            await using var session = store.LightweightSession();
            session.Events.Append(dispatched.Id, dispatched);
            await session.SaveChangesAsync();
            return Ok(dispatched);
        }

        [HttpPost("{orderId:guid}/outfordelivery")]
        public async Task<IActionResult> OutForDelivery(Guid orderId)
        {
            var outForDelivery = new Events.OrderOutForDelivery
            {
                Id = orderId,
                OutForDeliveryAtUtc = DateTime.UtcNow
            };
            await using var session = store.LightweightSession();
            session.Events.Append(outForDelivery.Id, outForDelivery);
            await session.SaveChangesAsync();
            return Ok(outForDelivery);
        }

        [HttpPost("{orderId:guid}/delivered")]
        public async Task<IActionResult> Delivered(Guid orderId)
        {
            var delivered = new Events.OrderDelivered
            {
                Id = orderId,
                DeliveredAtUtc = DateTime.UtcNow
            };
            await using var session = store.LightweightSession();
            session.Events.Append(delivered.Id, delivered);
            await session.SaveChangesAsync();
            return Ok(delivered);
        }

    }
}
