namespace EventSourcingMarten.Controllers;

public record CreateOrderCommand(string ProductName, string DeliveryAddress);