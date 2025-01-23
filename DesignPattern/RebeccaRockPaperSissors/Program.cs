Console.WriteLine("Double Dispatch");

public class Rock : IGameObject
{
    public bool Beats(IGameObject o)
    {
        // the receiver (this object) is a Rock. Ask the argument about rocks.
        return o.BeatsRock();
    }

    public bool BeatsRock()
    {
        // could return either false or true
        return false;
    }

    public bool BeatsPaper()
    {
        // a Rock doesn't beat a Paper
        return false;
    }

    public bool BeatsScissors()
    {
        // a Rock beats Scissors!
        return true;
    }
}

public class Paper : IGameObject
{
    public bool Beats(IGameObject o)
    {
        return o.BeatsPaper();
    }

    public bool BeatsRock()
    {
        return true;
    }

    public bool BeatsPaper()
    {
        return false;
    }

    public bool BeatsScissors()
    {
        return false;
    }
}

public class Scissors : IGameObject
{
    public bool Beats(IGameObject o)
    {
        return o.BeatsScissors();
    }

    public bool BeatsRock()
    {
        return false;
    }

    public bool BeatsPaper()
    {
        return false;
    }

    public bool BeatsScissors()
    {
        return true;
    }
}

public interface IGameObject
{
    public bool Beats(IGameObject o);
    public bool BeatsRock();
    public bool BeatsPaper();
    public bool BeatsScissors();
}