/*
 * Problem: You have an algorithm that varies depending on the context.
 * You want to easily switch between these algorithms at runtime without modifying the core logic.
 * 
 */

namespace ConsoleApp8.Strategy
{
   public class Order { }
}
namespace ConsoleApp8.Strategy.WithoutPattern
{
    public class OrderProcessor
    {
        public void ProcessOrder(Order order, string shippingMethod)
        {
            if (shippingMethod == "UPS")
            {
                // UPS shipping logic
            }
            else if (shippingMethod == "FedEx")
            {
                // FedEx shipping logic
            }
            // ... more shipping methods
        }
    }
}

namespace ConsoleApp8.Strategy.WithPattern
{
    // Strategy interface
    public interface IShippingStrategy
    {
        void Ship(Order order);
    }

// Concrete strategies
    public class UPSShipping : IShippingStrategy
    {
        public void Ship(Order order) { /* UPS logic */ }
    }

    public class FedExShipping : IShippingStrategy
    {
        public void Ship(Order order) { /* FedEx logic */ }
    }

    public class OrderProcessor
    {
        private IShippingStrategy _shippingStrategy;

        public OrderProcessor(IShippingStrategy shippingStrategy)
        {
            _shippingStrategy = shippingStrategy;
        }

        public void ProcessOrder(Order order)
        {
            _shippingStrategy.Ship(order);
        }
    }

// Usage
    class Consumer
    {
        void Run()
        {
            var processor = new OrderProcessor(new UPSShipping());
            Order order = new ();
            processor.ProcessOrder(order);

            processor = new OrderProcessor(new FedExShipping());
            processor.ProcessOrder(order);
        }
    }
}