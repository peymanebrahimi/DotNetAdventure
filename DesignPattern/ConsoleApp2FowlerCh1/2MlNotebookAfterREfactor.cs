namespace ConsoleApp2FowlerCh1;

public abstract class PerformanceCalculator(Performance performance, Play play)
{
    protected Performance Performance { get; private set; } = performance;
    protected Play Play { get; private set; } = play;

    public abstract decimal CalculateAmount();
    public abstract int CalculateVolumeCredits();
}

public class TragedyCalculator(Performance performance, Play play) : PerformanceCalculator(performance, play)
{
    public override decimal CalculateAmount()
    {
        decimal amount = 40000;
        if (Performance.Audience > 30)
        {
            amount += 1000 * (Performance.Audience - 30);
        }
        return amount;
    }

    public override int CalculateVolumeCredits()
    {
        return Math.Max(Performance.Audience - 30, 0);
    }
}

// Similar classes for ComedyCalculator, etc.

internal class ComedyCalculator(Performance performance, Play play) : PerformanceCalculator(performance, play)
{
    public override decimal CalculateAmount()
    {
        throw new NotImplementedException();
    }

    public override int CalculateVolumeCredits()
    {
        throw new NotImplementedException();
    }
}


public class StatementPrinter2
{
    private Dictionary<string, Play> plays;

    public StatementPrinter2(Dictionary<string, Play> plays)
    {
        this.plays = plays;
    }

    public string Statement(Invoice invoice)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        string result = $"Statement for {invoice.Customer}\n";

        foreach (Performance perf in invoice.Performances)
        {
            Play play = plays[perf.PlayID];
            PerformanceCalculator calculator = CreatePerformanceCalculator(perf, play); // Factory method

            decimal thisAmount = calculator.CalculateAmount();
            volumeCredits += calculator.CalculateVolumeCredits();

            result += $"  {play.Name}: {thisAmount / 100:C2} ({perf.Audience} seats)\n";
            totalAmount += thisAmount;
        }

        result += $"Amount owed is {totalAmount / 100:C2}\n";
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    private PerformanceCalculator CreatePerformanceCalculator(Performance performance, Play play)
    {
        switch (play.Type)
        {
            case "tragedy": return new TragedyCalculator(performance, play);
            case "comedy": return new ComedyCalculator(performance, play);
            // Add cases for other play types
            default: throw new ArgumentException($"Unknown type: {play.Type}");
        }
    }
}