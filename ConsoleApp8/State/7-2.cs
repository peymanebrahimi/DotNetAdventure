namespace ConsoleApp8.State;

// 1. Order Entity (Data from Database)
public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string Status { get; set; } // Store the state as a string in the DB
    public decimal TotalAmount { get; set; }
}

// 2. State Interface
public interface IOrderState
{
    void Process(Order order, IOrderService orderService, IPaymentService paymentService);
}

// 3. Concrete States
public class OrderCreatedState : IOrderState
{
    public void Process(Order order, IOrderService orderService, IPaymentService paymentService)
    {
        Console.WriteLine($"Order {order.Id}: Processing payment...");
        if (paymentService.ProcessPayment(order.TotalAmount))
        {
            order.Status = "Paid";
            orderService.UpdateOrderStatus(order); // Update DB
            Console.WriteLine($"Order {order.Id}: Payment successful. Order status updated to Paid.");
            order.Status = "Shipped"; // Transition to next state directly for simplicity
            orderService.UpdateOrderStatus(order);
            Console.WriteLine($"Order {order.Id}: Order status updated to Shipped.");
        }
        else
        {
            order.Status = "PaymentFailed";
            orderService.UpdateOrderStatus(order);
            Console.WriteLine($"Order {order.Id}: Payment failed. Order status updated to PaymentFailed.");
        }
    }
}

public class OrderShippedState : IOrderState
{
    public void Process(Order order, IOrderService orderService, IPaymentService paymentService)
    {
        Console.WriteLine($"Order {order.Id}: Order is already shipped.");
    }
}

public class OrderPaymentFailedState : IOrderState
{
    public void Process(Order order, IOrderService orderService, IPaymentService paymentService)
    {
        Console.WriteLine($"Order {order.Id}: Payment failed. Please contact customer support.");
    }
}

// 4. Services (Simulated)
public interface IOrderService
{
    Order GetOrder(int orderId);
    void UpdateOrderStatus(Order order);
}

public class OrderService : IOrderService
{
    public Order GetOrder(int orderId)
    {
        // Simulate DB retrieval
        return new Order {Id = orderId, CustomerName = "John Doe", Status = "Created", TotalAmount = 100};
    }

    public void UpdateOrderStatus(Order order)
    {
        // Simulate DB update
        Console.WriteLine($"Updating order {order.Id} status to {order.Status} in database.");
    }
}

public interface IPaymentService
{
    bool ProcessPayment(decimal amount);
}

public class PaymentService : IPaymentService
{
    public bool ProcessPayment(decimal amount)
    {
        // Simulate payment processing (could be a call to a payment gateway)
        return true; // Simulate successful payment
    }
}

// 5. Order Context (Manages the State)
public class OrderContext
{
    private Order _order;
    private IOrderState _currentState;
    private readonly IOrderService _orderService;
    private readonly IPaymentService _paymentService;

    public OrderContext(IOrderService orderService, IPaymentService paymentService)
    {
        _orderService = orderService;
        _paymentService = paymentService;
    }

    public void ProcessOrder(int orderId)
    {
        _order = _orderService.GetOrder(orderId);

        // Load state from DB
        _currentState = GetStateFromString(_order.Status);

        _currentState.Process(_order, _orderService, _paymentService);
    }

    private IOrderState GetStateFromString(string status)
    {
        switch (status)
        {
            case "Created": return new OrderCreatedState();
            case "Shipped": return new OrderShippedState();
            case "PaymentFailed": return new OrderPaymentFailedState();
            default: return new OrderCreatedState(); // Default if status is unknown
        }
    }
}

// Usage
class Consumer
{
    void Run()
    {
        var orderService = new OrderService();
        var paymentService = new PaymentService();
        var orderContext = new OrderContext(orderService, paymentService);

        orderContext.ProcessOrder(1);
    }
}