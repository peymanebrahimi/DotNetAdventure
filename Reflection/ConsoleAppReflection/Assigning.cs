namespace ConsoleAppReflection;

public class Assigning
{
    public static void Run()
    {
        Console.WriteLine(typeof(Child).BaseType?.Name);
        Console.WriteLine(typeof(Child).GetInterfaces()[0].Name);

        Parent p = new Parent();
        Console.WriteLine(p is IMyinterface); //static

        Type typeOfMyinterface = typeof(IMyinterface);
        Console.WriteLine(typeOfMyinterface.IsInstanceOfType(p)); //dynamic

        Type parentType = p.GetType();
        Console.WriteLine(typeOfMyinterface.IsAssignableFrom(parentType));
        IMyinterface a = p;
        //IFormattable f = p;//compile time error

        Console.WriteLine(parentType.IsAssignableFrom(typeof(Child)));
        p = new Child();

        Console.WriteLine(parentType.IsAssignableTo(typeOfMyinterface));
    }
}
public interface IMyinterface { }

public class Parent : IMyinterface { }

public class Child : Parent { }