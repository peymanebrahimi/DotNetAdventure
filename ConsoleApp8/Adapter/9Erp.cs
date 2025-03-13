/*
 * Key improvements and explanations for ERP context:

Legacy System Simulation: The LegacyProductData class represents data coming from an older ERP system or an external service. It has a different structure and data types than what the new system expects.
Target Interface: The IProduct interface defines the standard format that the new ERP system uses for product information.
Adapter: The ProductDataAdapter class is the adapter. It takes a LegacyProductData object as input and implements the IProduct interface. This class is responsible for translating the legacy data into the format expected by the new system. Crucially, it bridges the gap.
ERP Component: The ERPProductCatalog class represents a component of the new ERP system. It works with the IProduct interface, so it can seamlessly use the adapted product data.
Data Type Conversion: The adapter handles data type conversions (e.g., double to decimal for currency). Using decimal is very important for financial calculations to avoid rounding errors.
Handling Missing Data: The legacy system might not have all the information the new system needs (e.g., a product description). The adapter can provide default values or fetch the missing information from other sources (e.g., a database lookup). The example shows a placeholder.
Clear Example: The Main method demonstrates how to use the adapter to integrate legacy product data into the new ERP system. It shows how you take data from the old system, wrap it in the adapter, and then use it with the new system's components without having to change the new system at all.
This example clearly demonstrates the adapter pattern's role in integrating different systems or components within an ERP environment. It highlights how the adapter acts as a translator, allowing the new system to work with legacy data without requiring changes to either the old or new system's core code. This promotes maintainability and reduces the risk of introducing bugs.
 */
namespace ConsoleApp8.Adaptor;

using System;

// Existing System (Legacy ERP component or external service)
public class LegacyProductData
{
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }

    public LegacyProductData(string name, double price)
    {
        ProductName = name;
        ProductPrice = price;
    }
}

// Target Interface (What the new ERP system expects)
public interface IProduct
{
    string GetName();
    decimal GetPrice(); // Using decimal for currency is generally recommended
    string GetDescription();
}

// Adapter Class (Adapts LegacyProductData to IProduct interface)
public class ProductDataAdapter : IProduct
{
    private LegacyProductData _legacyProduct;

    public ProductDataAdapter(LegacyProductData legacyProduct)
    {
        _legacyProduct = legacyProduct;
    }

    public string GetName()
    {
        return _legacyProduct.ProductName;
    }

    public decimal GetPrice()
    {
        return (decimal)_legacyProduct.ProductPrice; // Convert to decimal
    }

    public string GetDescription()
    {
        // You might need to fetch/generate a description since the legacy system doesn't provide it directly.
        // This could involve a database lookup, API call, or some default value.
        return $"Description for {_legacyProduct.ProductName} (from legacy system)"; // Placeholder
    }
}


// New ERP System Component (Uses the IProduct interface)
public class ERPProductCatalog
{
    public void DisplayProductInfo(IProduct product)
    {
        Console.WriteLine($"Product Name: {product.GetName()}");
        Console.WriteLine($"Product Price: {product.GetPrice():C}"); // Format as currency
        Console.WriteLine($"Product Description: {product.GetDescription()}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Example: Integrating legacy product data into the new ERP system

        // 1. Get data from the legacy system
        LegacyProductData legacyData = new LegacyProductData("Old Widget", 99.99);

        // 2. Create an adapter to make it compatible with the new system
        IProduct adaptedProduct = new ProductDataAdapter(legacyData);

        // 3. Use the adapted product in the new ERP system
        ERPProductCatalog catalog = new ERPProductCatalog();
        catalog.DisplayProductInfo(adaptedProduct);


        // Another Example
        LegacyProductData legacyData2 = new LegacyProductData("Legacy Gadget", 150);
        IProduct adaptedProduct2 = new ProductDataAdapter(legacyData2);
        catalog.DisplayProductInfo(adaptedProduct2);

        Console.ReadKey();
    }
}