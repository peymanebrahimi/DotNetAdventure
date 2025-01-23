namespace ConsoleApp1Visitor.Gemini.WithPattern;

public abstract class Book
{
    public string Title { get; set; }
    public double Price { get; set; }
    
    public abstract void Accept(IBookVisitor bookVisitor);
}
public class Novel : Book
{
    public override void Accept(IBookVisitor bookVisitor)
    {
        bookVisitor.Visit(this);
    }
}
public class TechnicalBook : Book
{
    public override void Accept(IBookVisitor bookVisitor)
    {
        bookVisitor.Visit(this);
    }
}
//use bookVisitor pattern for calculate price
public interface IBookVisitor
{
    void Visit(Novel novel);
    void Visit(TechnicalBook technicalBook);
}
public abstract class BookVisitorBase : IBookVisitor
{
    protected double CalculateBasePrice(Book book) 
    {
        // Common price calculation logic 
        // (e.g., apply a general tax)
        return book.Price * 1.1; // Example: 10% tax
    }

    public abstract void Visit(Novel novel);
    public abstract void Visit(TechnicalBook technicalBook);
}
public class PriceBookVisitor(/*inject whatever u want*/) : BookVisitorBase
{
    public double TotalPrice { get; private set; }
    public override void Visit(Novel novel)
    {
        // Calculate price for Novel within the visitor
        TotalPrice += CalculateBasePrice(novel) * 0.9; // 10% discount for Novels
    }
    public override void Visit(TechnicalBook technicalBook)
    {
        // Calculate price for TechnicalBook within the visitor
        TotalPrice += CalculateBasePrice(technicalBook); 
    }
    // ... other Visit methods
}

// کلاس مشتری
public class Customer
{
    public List<Book> Books { get; set; }
    public double CalculateTotalPrice()
    {
        PriceBookVisitor bookVisitor = new PriceBookVisitor();
        foreach (var book in Books)
        {
            book.Accept(bookVisitor);
        }
        return bookVisitor.TotalPrice;
    }
}

public class DiscountVisitor : BookVisitorBase
{
    public double TotalDiscount { get; private set; }

    public override void Visit(Novel novel)
    {
        TotalDiscount += CalculateBasePrice(novel) * 0.05; // 5% discount for Novels
    }

    public override void Visit(TechnicalBook technicalBook)
    {
        TotalDiscount += CalculateBasePrice(technicalBook) * 0.02; // 2% discount for TechnicalBooks
    }

    // ... other Visit methods
}