// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

foreach (var item in Data.data)
    Console.WriteLine(item);

Console.WriteLine("\n***********************************\n");

var heatingDegreeDays = new HeatingDegreeDays(65, Data.data);
Console.WriteLine(heatingDegreeDays);

var coolingDegreeDays = new CoolingDegreeDays(65, Data.data);
Console.WriteLine(coolingDegreeDays);

Console.WriteLine("\n***********************************\n");

var phoneNumbers = new string[2];
Person person1 = new("Nancy", "Davolio", phoneNumbers);
Person person2 = new("Nancy", "Davolio", phoneNumbers);
Console.WriteLine(person1 == person2); // output: True

person1.PhoneNumbers[0] = "555-1234";
Console.WriteLine(person1 == person2); // output: True

Console.WriteLine(ReferenceEquals(person1, person2)); // output: False

Console.ReadLine();

public readonly record struct DailyTemperature(double HighTemp, double LowTemp)
{
    public double Mean => (HighTemp + LowTemp) / 2.0;
}

class Data
{
    public static DailyTemperature[] data = new DailyTemperature[]
        {
            new DailyTemperature(HighTemp: 57, LowTemp: 30),
            new DailyTemperature(60, 35),
            new DailyTemperature(63, 33),
            new DailyTemperature(68, 29),
            new DailyTemperature(72, 47),
            new DailyTemperature(75, 55),
            new DailyTemperature(77, 55),
            new DailyTemperature(72, 58),
            new DailyTemperature(70, 47),
            new DailyTemperature(77, 59),
            new DailyTemperature(85, 65),
            new DailyTemperature(87, 65),
            new DailyTemperature(85, 72),
            new DailyTemperature(83, 68),
            new DailyTemperature(77, 65),
            new DailyTemperature(72, 58),
            new DailyTemperature(77, 55),
            new DailyTemperature(76, 53),
            new DailyTemperature(80, 60),
            new DailyTemperature(85, 66)
        };
}


public abstract record DegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords);

public sealed record HeatingDegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemperature, TempRecords)
{
    public double DegreeDays => TempRecords
                            .Where(s => s.Mean < BaseTemperature)
                            .Sum(s => BaseTemperature - s.Mean);
}

public sealed record CoolingDegreeDays(double BaseTemperature, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemperature, TempRecords)
{
    public double DegreeDays => TempRecords
                            .Where(s => s.Mean > BaseTemperature)
                            .Sum(s => s.Mean - BaseTemperature);
}

public record Person(string FirstName, string LastName, string[] PhoneNumbers);
