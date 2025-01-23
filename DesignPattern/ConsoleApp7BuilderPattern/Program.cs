namespace ConsoleApp7BuilderPattern;

class Program
{
    static void Main(string[] args)
    {
        var url = new MyUrlBuilder()
            .WithHost("bing.com")
            .WithProtocol("https")
            .Build();

        Console.WriteLine(url);


        url = MyUrlBuilder2.Create()
            .WithHost("google.com")
            .WithProtocol("http")
            .Build();

        Console.WriteLine(url);

        Console.ReadLine();
    }
}
