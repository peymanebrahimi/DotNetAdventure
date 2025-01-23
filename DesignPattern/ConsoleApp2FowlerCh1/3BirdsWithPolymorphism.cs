namespace ConsoleApp2FowlerCh1;

// copilot
// chapter 10
// REPLACE CONDITIONAL WITH POLYMORPHISM
//page 301
public class Program3
{
    public static void Main()
    {
        var birds = new List<Bird>
        {
            new Bird { Name = "Bird1", Type = "EuropeanSwallow" },
            new Bird { Name = "Bird2", Type = "AfricanSwallow", NumberOfCoconuts = 3 },
            new Bird { Name = "Bird3", Type = "NorwegianBlueParrot", Voltage = 150, IsNailed = false }
        };

        var processor = new BirdProcessor(birds);
        var plumages = processor.GetPlumages();
        var airSpeeds = processor.GetAirSpeeds();

        Console.WriteLine("Plumages:");
        foreach (var plumage in plumages)
        {
            Console.WriteLine($"{plumage.Key}: {plumage.Value}");
        }

        Console.WriteLine("\nAir Speeds:");
        foreach (var airSpeed in airSpeeds)
        {
            Console.WriteLine($"{airSpeed.Key}: {airSpeed.Value}");
        }
    }
}

public class Bird
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int NumberOfCoconuts { get; set; }
    public int Voltage { get; set; }
    public bool IsNailed { get; set; }
}
public interface IPlumageStrategy
{
    string GetPlumage(Bird bird);
}

public interface IAirSpeedStrategy
{
    int? GetAirSpeed(Bird bird);
}

public class EuropeanSwallowPlumage : IPlumageStrategy
{
    public string GetPlumage(Bird bird) => "average";
}

public class AfricanSwallowPlumage : IPlumageStrategy
{
    public string GetPlumage(Bird bird) => bird.NumberOfCoconuts > 2 ? "tired" : "average";
}

public class NorwegianBlueParrotPlumage : IPlumageStrategy
{
    public string GetPlumage(Bird bird) => bird.Voltage > 100 ? "scorched" : "beautiful";
}

public class EuropeanSwallowSpeed : IAirSpeedStrategy
{
    public int? GetAirSpeed(Bird bird) => 35;
}

public class AfricanSwallowSpeed : IAirSpeedStrategy
{
    public int? GetAirSpeed(Bird bird) => 40 - 2 * bird.NumberOfCoconuts;
}

public class NorwegianBlueParrotSpeed : IAirSpeedStrategy
{
    public int? GetAirSpeed(Bird bird) => bird.IsNailed ? 0 : 10 + bird.Voltage / 10;
}
public static class BirdStrategyFactory
{
    public static IPlumageStrategy CreatePlumageStrategy(string birdType)
    {
        return birdType switch
        {
            "EuropeanSwallow" => new EuropeanSwallowPlumage(),
            "AfricanSwallow" => new AfricanSwallowPlumage(),
            "NorwegianBlueParrot" => new NorwegianBlueParrotPlumage(),
            _ => throw new ArgumentException($"Unknown bird type: {birdType}")
        };
    }

    public static IAirSpeedStrategy CreateAirSpeedStrategy(string birdType)
    {
        return birdType switch
        {
            "EuropeanSwallow" => new EuropeanSwallowSpeed(),
            "AfricanSwallow" => new AfricanSwallowSpeed(),
            "NorwegianBlueParrot" => new NorwegianBlueParrotSpeed(),
            _ => throw new ArgumentException($"Unknown bird type: {birdType}")
        };
    }
}
public class BirdProcessor
{
    private readonly IEnumerable<Bird> _birds;

    public BirdProcessor(IEnumerable<Bird> birds)
    {
        _birds = birds;
    }

    public Dictionary<string, string> GetPlumages()
    {
        return _birds.ToDictionary(
            bird => bird.Name, 
            bird =>
            {
                var plumageStrategy = BirdStrategyFactory.CreatePlumageStrategy(bird.Type);
                return plumageStrategy.GetPlumage(bird);
            }
        );
    }

    public Dictionary<string, int?> GetAirSpeeds()
    {
        return _birds.ToDictionary(
            bird => bird.Name, 
            bird =>
            {
                var airSpeedStrategy = BirdStrategyFactory.CreateAirSpeedStrategy(bird.Type);
                return airSpeedStrategy.GetAirSpeed(bird);
            }
        );
    }
}


