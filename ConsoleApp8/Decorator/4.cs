/*
 * Problem: You want to dynamically add responsibilities to an object without modifying its class.
 */
namespace ConsoleApp8.Decorator.WithoutPattern
{
    public class Car
    {
        public virtual void Drive() => Console.WriteLine("Car is driving.");
    }

    public class SportsCar : Car
    {
        public override void Drive() => Console.WriteLine("Sports car is driving fast.");
    }

    public class LuxuryCar : Car
    {
        public override void Drive() => Console.WriteLine("Luxury car is driving smoothly.");
    }
}

namespace ConsoleApp8.Decorator.WithPattern
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public virtual void Drive() => Console.WriteLine("Car is driving.");
    }

    public abstract class CarDecorator(ICar car) : ICar
    {
        public virtual void Drive() => car.Drive();
    }

    public class SportsCarDecorator(ICar car) : CarDecorator(car)
    {
        public override void Drive() 
        { 
            base.Drive(); 
            Console.WriteLine("Sports car enhancement."); 
        }
    }

    public class LuxuryCarDecorator(ICar car) : CarDecorator(car)
    {
        public override void Drive() 
        { 
            base.Drive(); 
            Console.WriteLine("Luxury car enhancement."); 
        }
    }

// Usage
    class Consumer
    {
        void Run()
        {
            ICar basicCar = new Car();
            ICar sportsCar = new SportsCarDecorator(basicCar);
            sportsCar.Drive();
        }
    }
}