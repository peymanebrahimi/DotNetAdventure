// See https://aka.ms/new-console-template for more information

using Mapster;

Console.WriteLine("Hello, World!");


try
{
    var child = new AddressCt() {Age = 20, Name = "John"};
    var ct = child.Adapt(typeof(AddressCt), typeof(TdAddress));
    var s = ct as IBaseSql;
    var mm = s.GetType().GetMethod("Execute");
    mm.Invoke(ct, null);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

internal interface IBaseSql
{
    void Execute();
}

class BaseSql : IBaseSql
{
    public int BaseSqlId { get; set; }

    public void Execute()
    {
        Console.WriteLine("Execute from parrent");
    }
}

class TdAddress : BaseSql
{
    public int Age { get; set; }
    public string Name { get; set; }
}

class AddressCt
{
    public int Age { get; set; }
    public string Name { get; set; }
}