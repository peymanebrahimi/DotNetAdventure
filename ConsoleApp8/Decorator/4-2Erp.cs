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