namespace ConsoleApp8.Decorator.WithoutPattern2
{
    public class Coffee
    {
        public decimal Cost { get; set; } = 1.00m; 
    }

    public class CoffeeWithMilk(Coffee coffee)
    {
        public decimal Cost { get; set; } = coffee.Cost + 0.50m;
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

    public abstract class CondimentDecorator(IBeverage beverage) : IBeverage
    {
        protected readonly IBeverage Beverage = beverage;

        public abstract string Description { get; } 
        public abstract decimal Cost { get; }
    }

    public class Milk(IBeverage beverage) : CondimentDecorator(beverage)
    {
        public override string Description => Beverage.Description + ", Milk";
        public override decimal Cost => Beverage.Cost + 0.50m;
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