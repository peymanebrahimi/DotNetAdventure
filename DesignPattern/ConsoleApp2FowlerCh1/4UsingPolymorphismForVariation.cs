namespace ConsoleApp2FowlerCh1;

//chapter 10
//Using Polymorphism for Variation

public class Voyage
{
    public string Zone { get; set; }
    public int Length { get; set; }
}

public class HistoryRecord
{
    public string Zone { get; set; }
    public int Profit { get; set; }
}
public interface IRiskCalculator
{
    int CalculateRisk(Voyage voyage, List<HistoryRecord> history);
}

public interface IProfitFactorCalculator
{
    int CalculateProfitFactor(Voyage voyage, List<HistoryRecord> history);
}
public class VoyageRiskCalculator : IRiskCalculator
{
    public int CalculateRisk(Voyage voyage, List<HistoryRecord> history)
    {
        int result = 1;
        if (voyage.Length > 4) result += 2;
        if (voyage.Length > 8) result += voyage.Length - 8;
        if (new[] { "china", "eastindies" }.Contains(voyage.Zone)) result += 4;
        return Math.Max(result, 0);
    }
}

public class CaptainHistoryRiskCalculator : IRiskCalculator
{
    public int CalculateRisk(Voyage voyage, List<HistoryRecord> history)
    {
        int result = 1;
        if (history.Count < 5) result += 4;
        result += history.Count(v => v.Profit < 0);
        if (voyage.Zone == "china" && history.Any(v => v.Zone == "china")) result -= 2;
        return Math.Max(result, 0);
    }
}
public class VoyageProfitFactorCalculator : IProfitFactorCalculator
{
    public int CalculateProfitFactor(Voyage voyage, List<HistoryRecord> history)
    {
        int result = 2;
        if (voyage.Zone == "china") result += 1;
        if (voyage.Zone == "eastindies") result += 1;
        if (voyage.Zone == "china" && history.Any(v => v.Zone == "china"))
        {
            result += 3;
            if (history.Count > 10) result += 1;
            if (voyage.Length > 12) result += 1;
            if (voyage.Length > 18) result -= 1;
        }
        else
        {
            if (history.Count > 8) result += 1;
            if (voyage.Length > 14) result -= 1;
        }
        return result;
    }
}
public class RatingCalculator
{
    private readonly IRiskCalculator _voyageRiskCalculator;
    private readonly IRiskCalculator _captainHistoryRiskCalculator;
    private readonly IProfitFactorCalculator _voyageProfitFactorCalculator;

    public RatingCalculator(IRiskCalculator voyageRiskCalculator, 
        IRiskCalculator captainHistoryRiskCalculator, 
        IProfitFactorCalculator voyageProfitFactorCalculator)
    {
        _voyageRiskCalculator = voyageRiskCalculator;
        _captainHistoryRiskCalculator = captainHistoryRiskCalculator;
        _voyageProfitFactorCalculator = voyageProfitFactorCalculator;
    }

    public string CalculateRating(Voyage voyage, List<HistoryRecord> history)
    {
        var vpf = _voyageProfitFactorCalculator.CalculateProfitFactor(voyage, history);
        var vr = _voyageRiskCalculator.CalculateRisk(voyage, history);
        var chr = _captainHistoryRiskCalculator.CalculateRisk(voyage, history);

        return (vpf * 3 > (vr + chr * 2)) ? "A" : "B";
    }
}
public class Program4
{
    public static void Main()
    {
        var voyage = new Voyage { Zone = "westindies", Length = 10 };
        var history = new List<HistoryRecord>
        {
            new HistoryRecord { Zone = "eastindies", Profit = 5 },
            new HistoryRecord { Zone = "westindies", Profit = 15 },
            new HistoryRecord { Zone = "china", Profit = 2 },
            new HistoryRecord { Zone = "westafrica", Profit = 7 }
        };

        var voyageRiskCalculator = new VoyageRiskCalculator();
        var captainHistoryRiskCalculator = new CaptainHistoryRiskCalculator();
        var voyageProfitFactorCalculator = new VoyageProfitFactorCalculator();

        var ratingCalculator = new RatingCalculator(voyageRiskCalculator, captainHistoryRiskCalculator, voyageProfitFactorCalculator);
        var myRating = ratingCalculator.CalculateRating(voyage, history);

        Console.WriteLine($"Voyage rating is: {myRating}");
    }
}
