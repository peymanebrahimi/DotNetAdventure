using System.Globalization;

namespace ConsoleApp2FowlerCh1;
/*
 Image a company of theatrical players who go out to various events performing plays.
Typically, a customer will request a few plays and the company charges them based on
the size of the audience and the kind of play they perform. There are currently two
kinds of plays that the company performs: tragedies and comedies. As well as providing
a bill for the performance, the company gives its customers “volume credits” which they
can use for discounts on future performances—think of it as a customer loyalty
mechanism.
 */
public class StatementPrinter
{
    private Dictionary<string, Play> plays;

    public StatementPrinter(Dictionary<string, Play> plays)
    {
        this.plays = plays;
    }

    public string Statement(Invoice invoice)
    {
        decimal totalAmount = 0;
        int volumeCredits = 0;
        string result = $"Statement for {invoice.Customer}\n";

        CultureInfo culture = new CultureInfo("en-US");
        NumberFormatInfo format = culture.NumberFormat;

        foreach (Performance perf in invoice.Performances)
        {
            Play play = plays[perf.PlayID];
            decimal thisAmount = 0;

            switch (play.Type)
            {
                case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30)
                    {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20)
                    {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new ArgumentException($"Unknown type: {play.Type}");
            }

            // Add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);

            // Add extra credit for every ten comedy attendees
            if (play.Type == "comedy")
            {
                volumeCredits += perf.Audience / 5;
            }

            // Print line for this order
            result += $"  {play.Name}: {thisAmount / 100:C2} ({perf.Audience} seats)\n";
            totalAmount += thisAmount;
        }

        result += $"Amount owed is {totalAmount / 100:C2}\n";
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }
}
