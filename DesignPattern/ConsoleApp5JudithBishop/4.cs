namespace ConsoleApp5JudithBishop;

interface IRingMode
{
    void Handle(Mobile mobile);
}

class Silent : IRingMode
{
    public void Handle(Mobile mobile)
    {
        Console.WriteLine("Show silent icon on mobile");
    }
}

class Vibration : IRingMode
{
    public void Handle(Mobile mobile)
    {
        throw new NotImplementedException();
    }
}

class Ringing : IRingMode
{
    public void Handle(Mobile mobile)
    {
        throw new NotImplementedException();
    }
}

class Mobile
{
    IRingMode _ringMode = new Ringing();
    public void SetRingMode(IRingMode ringMode) => _ringMode = ringMode;
}