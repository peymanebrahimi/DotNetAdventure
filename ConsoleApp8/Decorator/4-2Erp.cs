/*
 * Key improvements and explanations for ERP context:

Clearer Product Hierarchy: The IProduct, BaseProduct, and ProductDecorator structure is more robust and clearly defines the component and decorator roles.
Realistic Decorators: The example decorators (Warranty, Installation, Gift Wrapping) are more relevant to an ERP system, especially for handling products with optional services or add-ons. You can easily add more (e.g., Shipping, Rush Order, etc.).
ERPOrder Class: This class simulates a simplified order processing system within an ERP. It holds a collection of IProduct objects (which can be base products or decorated products). This is crucial for demonstrating how decorators are used in a real-world scenario.
Order Processing Logic: The ProcessOrder method in ERPOrder iterates through the products in the order and calculates the total price, demonstrating how the decorators' added functionality (price and description changes) are applied during order processing.
Example Usage: The Main method clearly shows how to create a base product and then dynamically add decorators to it. The ERP order example shows how you would use these decorated products in a typical ERP workflow.
Flexibility: The decorator pattern allows you to combine different enhancements in any order without modifying the core BaseProduct class. This is extremely important in ERP systems where product configurations can be complex.
 */
namespace ConsoleApp8.Decorator;

using System;
using System.Collections.Generic;

// Component Interface (Product)
public interface IProduct
{
    string GetDescription();
    double GetPrice();
}

// Concrete Component (Base Product)
public class BaseProduct : IProduct
{
    public string Name { get; set; }
    public double Price { get; set; }

    public BaseProduct(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public string GetDescription()
    {
        return Name;
    }

    public double GetPrice()
    {
        return Price;
    }
}

// Abstract Decorator
public abstract class ProductDecorator : IProduct
{
    protected IProduct _product;

    public ProductDecorator(IProduct product)
    {
        _product = product;
    }

    public virtual string GetDescription()
    {
        return _product.GetDescription();
    }

    public virtual double GetPrice()
    {
        return _product.GetPrice();
    }
}

// Concrete Decorators (Enhancements)

public class WarrantyDecorator : ProductDecorator
{
    private int _warrantyMonths;
    private double _warrantyCostPercentage;

    public WarrantyDecorator(IProduct product, int warrantyMonths, double warrantyCostPercentage) : base(product)
    {
        _warrantyMonths = warrantyMonths;
        _warrantyCostPercentage = warrantyCostPercentage;
    }

    public override string GetDescription()
    {
        return $"{_product.GetDescription()} (with {_warrantyMonths}-month warranty)";
    }

    public override double GetPrice()
    {
        return _product.GetPrice() + (_product.GetPrice() * _warrantyCostPercentage);
    }
}

public class InstallationDecorator : ProductDecorator
{
    private double _installationFee;

    public InstallationDecorator(IProduct product, double installationFee) : base(product)
    {
        _installationFee = installationFee;
    }

    public override string GetDescription()
    {
        return $"{_product.GetDescription()} (with installation)";
    }

    public override double GetPrice()
    {
        return _product.GetPrice() + _installationFee;
    }
}

public class GiftWrappingDecorator : ProductDecorator
{
    private double _wrappingFee;

    public GiftWrappingDecorator(IProduct product, double wrappingFee) : base(product)
    {
        _wrappingFee = wrappingFee;
    }

    public override string GetDescription()
    {
        return $"{_product.GetDescription()} (gift wrapped)";
    }

    public override double GetPrice()
    {
        return _product.GetPrice() + _wrappingFee;
    }
}



// Example Usage in an ERP context (e.g., Order processing)
public class ERPOrder
{
    public List<IProduct> Products { get; set; }

    public ERPOrder()
    {
        Products = new List<IProduct>();
    }

    public void AddProduct(IProduct product)
    {
        Products.Add(product);
    }

    public void ProcessOrder()
    {
        double totalPrice = 0;
        foreach (var product in Products)
        {
            Console.WriteLine($"Product: {product.GetDescription()}, Price: ${product.GetPrice()}");
            totalPrice += product.GetPrice();
        }
        Console.WriteLine($"Total Order Price: ${totalPrice}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Example: Creating a product with decorators

        IProduct laptop = new BaseProduct("Gaming Laptop", 1200);
        laptop = new WarrantyDecorator(laptop, 12, 0.10); // 10% warranty cost
        laptop = new InstallationDecorator(laptop, 50);      // $50 installation fee
        laptop = new GiftWrappingDecorator(laptop, 10);      // $10 gift wrapping

        Console.WriteLine(laptop.GetDescription()); // Output: Gaming Laptop (with 12-month warranty) (with installation) (gift wrapped)
        Console.WriteLine(laptop.GetPrice());       // Output: 1442 (1200 + 120 + 50 + 10)

        // Example within an ERP Order
        ERPOrder order = new ERPOrder();
        order.AddProduct(laptop);

        IProduct mouse = new BaseProduct("Wireless Mouse", 25);
        order.AddProduct(mouse);

        order.ProcessOrder();



        Console.ReadKey();
    }
}