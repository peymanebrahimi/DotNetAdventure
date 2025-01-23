// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Text;

Console.WriteLine("Hello, World!");
//ch 1

var plays = new Dictionary<string, Play>
{
    {"hamlet", new Play {Name = "Hamlet", Type = "tragedy"}},
    {"aslike", new Play {Name = "As You Like It", Type = "comedy"}},
    {"othello", new Play {Name = "Othello", Type = "tragedy"}}
};

var invoices = new List<Invoice>
{
    new Invoice
    {
        Customer = "BigCo",
        Performances = new List<Performance>
        {
            new Performance {PlayID = "hamlet", Audience = 55},
            new Performance {PlayID = "aslike", Audience = 35},
            new Performance {PlayID = "othello", Audience = 40}
        }
    }
};

var processor = new InvoiceProcessor(plays);

foreach (var invoice in invoices)
{
    Console.WriteLine(processor.GenerateStatement(invoice));
}

public class Play
{
    public string Name { get; set; }
    public string Type { get; set; }
}

public class Performance
{
    public string PlayID { get; set; }
    public int Audience { get; set; }
}

public class Invoice
{
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; }
}

public interface IAmountCalculator
{
    double CalculateAmount(int audience);
}

public class TragedyAmountCalculator : IAmountCalculator
{
    public double CalculateAmount(int audience)
    {
        double amount = 40000;
        if (audience > 30)
        {
            amount += 1000 * (audience - 30);
        }

        return amount;
    }
}

public class ComedyAmountCalculator : IAmountCalculator
{
    public double CalculateAmount(int audience)
    {
        double amount = 30000;
        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }

        amount += 300 * audience;
        return amount;
    }
}

public static class AmountCalculatorFactory
{
    public static IAmountCalculator CreateCalculator(string playType)
    {
        return playType switch
        {
            "tragedy" => new TragedyAmountCalculator(),
            "comedy" => new ComedyAmountCalculator(),
            _ => throw new ArgumentException($"Unknown play type: {playType}")
        };
    }
}

public class InvoiceProcessor
{
    private readonly Dictionary<string, Play> _plays;
    private readonly Func<double, string> _format;

    public InvoiceProcessor(Dictionary<string, Play> plays)
    {
        _plays = plays;
        _format = amount => amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
    }

    public string GenerateStatement(Invoice invoice)
    {
        var totalAmount = 0.0;
        var volumeCredits = 0;
        var result = new StringBuilder($"Statement for {invoice.Customer}\n");

        foreach (var perf in invoice.Performances)
        {
            var play = _plays[perf.PlayID];
            var calculator = AmountCalculatorFactory.CreateCalculator(play.Type);
            var thisAmount = calculator.CalculateAmount(perf.Audience);

            // Add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            if (play.Type == "comedy")
            {
                volumeCredits += perf.Audience / 5;
            }

            // Print line for this order
            result.AppendLine($" {play.Name}: {_format(thisAmount / 100)} ({perf.Audience} seats)");
            totalAmount += thisAmount;
        }

        result.AppendLine($"Amount owed is {_format(totalAmount / 100)}");
        result.AppendLine($"You earned {volumeCredits} credits");
        return result.ToString();
    }
}