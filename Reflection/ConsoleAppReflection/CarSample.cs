// See https://aka.ms/new-console-template for more information

using System.Reflection;

public class CarSample()
{
    public static void Run()
    {
        var i = (int) Activator.CreateInstance(typeof(int));
        i = 10;
        Console.WriteLine(i);

        var date = (DateTime) Activator.CreateInstance(typeof(DateTime), 2023, 8, 12);
        Console.WriteLine(date);

//---------------cunstructorinfo------------

        ConstructorInfo ci = typeof(Car).GetConstructor(new[] {typeof(int)});
        var car = (Car) ci.Invoke(new Object[]{6});
        Console.WriteLine(car);
    }
}

public class Car
{
    public int WheelCount { get;  }
    public string Color { get;  }

    public Car() {}

    public Car(int wheelCount)
    {
        WheelCount = wheelCount;
        Color = "White";
    }

    public Car(int wheelCount, string color)
    {
        WheelCount = wheelCount;
        Color = color;
    }

    public override string ToString()
    {
        return $"car with {WheelCount} wheel and {Color} color";
    }
}
