namespace ConsoleAppReflection;

public class GenericTypeSample
{
    public static void Run()
    {
        Type closed = typeof(List<int>);
        List<int> list =(List<int>) Activator.CreateInstance(closed);

        Type unbound = typeof(List<>);

        Type closed2 = unbound.MakeGenericType(typeof(string));

        Type unbound2 = closed.GetGenericTypeDefinition();//unbound==unbound2

        Console.WriteLine(closed2.GetGenericArguments()[0]);

        Console.WriteLine(typeof(Car).IsGenericType);
    }
}