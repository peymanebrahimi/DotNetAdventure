namespace ConsoleApp8.Decorator.WithoutPattern2
{
    public class Coffee
    {
        public decimal Cost { get; set; } = 1.00m; 
    }

    public class CoffeeWithMilk
    {
        private Coffee _coffee;

        public CoffeeWithMilk(Coffee coffee)
        {
            _coffee = coffee;
            Cost = coffee.Cost + 0.50m; 
        }

        public decimal Cost { get; set; } 
    }
}

namespace ConsoleApp8.Decorator.WithPattern2
{
    public interface IBeverage
    {
        string Description { get; }
        decimal Cost { get; }
    }

    public class Coffee : IBeverage
    {
        public string Description => "Coffee";
        public decimal Cost { get; set; } = 1.00m; 
    }

    public abstract class CondimentDecorator : IBeverage
    {
        protected IBeverage _beverage;

        public CondimentDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public abstract string Description { get; } 
        public abstract decimal Cost { get; }
    }

    public class Milk : CondimentDecorator
    {
        public Milk(IBeverage beverage) : base(beverage) { }

        public override string Description => _beverage.Description + ", Milk";
        public override decimal Cost => _beverage.Cost + 0.50m;
    }

// Usage:
    class Consumer
    {
        void Run()
        {
            IBeverage coffee = new Coffee();
            coffee = new Milk(coffee);
        }
    }
}