/*
 * Key improvements and explanations for ERP context:

Product Hierarchy: The IProduct interface and concrete product classes (Laptop, Mouse, Keyboard) define the products in the system. You can easily add more product types.
Abstract Factory: The IProductFactory interface defines the contract for creating families of related products.
Concrete Factories: GamingProductFactory and OfficeProductFactory are concrete implementations that produce specific product families. This is where you define the relationships between products (e.g., a gaming laptop goes with a gaming mouse).
ERPWorkstationConfigurator: This class simulates a part of an ERP system responsible for configuring workstations. It uses the IProductFactory to create the appropriate products based on the type of workstation being configured. This is a good example of how a factory can be used to manage complex object creation in an ERP environment.
Clearer Example Usage: The Main method demonstrates how to use the factories to create different workstation configurations. It shows how you can switch between product families simply by using a different factory.
 */
namespace ConsoleApp8.Factory;

using System;

// Product Interface
public interface IProduct
{
    string GetName();
    double GetPrice();
    string GetDescription();
}

// Concrete Products
public class Laptop : IProduct
{
    public string GetName() => "Laptop";
    public double GetPrice() => 1200;
    public string GetDescription() => "High-performance laptop";
}

public class Mouse : IProduct
{
    public string GetName() => "Mouse";
    public double GetPrice() => 25;
    public string GetDescription() => "Wireless mouse";
}

public class Keyboard : IProduct
{
    public string GetName() => "Keyboard";
    public double GetPrice() => 75;
    public string GetDescription() => "Mechanical keyboard";
}


// Abstract Factory (defines the interface for creating product families)
public interface IProductFactory
{
    IProduct CreateComputer();
    IProduct CreatePeripheral();
}

// Concrete Factories (implement the abstract factory for specific product families)

public class GamingProductFactory : IProductFactory
{
    public IProduct CreateComputer() => new Laptop();  // Gaming Laptop
    public IProduct CreatePeripheral() => new Mouse(); // Gaming Mouse
}

public class OfficeProductFactory : IProductFactory
{
    public IProduct CreateComputer() => new Laptop(); // Office Laptop (could be a different laptop type)
    public IProduct CreatePeripheral() => new Keyboard(); // Ergonomic Keyboard
}

// Example Usage in an ERP system (e.g., configuring workstations)
public class ERPWorkstationConfigurator
{
    private IProductFactory _factory;

    public ERPWorkstationConfigurator(IProductFactory factory)
    {
        _factory = factory;
    }

    public void ConfigureWorkstation()
    {
        IProduct computer = _factory.CreateComputer();
        IProduct peripheral = _factory.CreatePeripheral();

        Console.WriteLine($"Workstation configured with: {computer.GetName()} ({computer.GetDescription()}) and {peripheral.GetName()} ({peripheral.GetDescription()})");
        Console.WriteLine($"Total Price: ${computer.GetPrice() + peripheral.GetPrice()}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Configuring a gaming workstation
        IProductFactory gamingFactory = new GamingProductFactory();
        ERPWorkstationConfigurator gamingConfigurator = new ERPWorkstationConfigurator(gamingFactory);
        gamingConfigurator.ConfigureWorkstation();

        Console.WriteLine();

        // Configuring an office workstation
        IProductFactory officeFactory = new OfficeProductFactory();
        ERPWorkstationConfigurator officeConfigurator = new ERPWorkstationConfigurator(officeFactory);
        officeConfigurator.ConfigureWorkstation();

        Console.ReadKey();
    }
}