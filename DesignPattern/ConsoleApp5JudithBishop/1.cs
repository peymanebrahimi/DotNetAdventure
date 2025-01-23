namespace ConsoleApp5JudithBishop;

// State Pattern Judith Bishop Oct 2007
// Shows two states with two operations, which themselves change the state
// Increments and decrements a counter in the context

interface IState
{
    int MoveUp(Context context);
    int MoveDown(Context context);
}

class NormalState : IState
{
    public int MoveUp(Context context)
    {
        context.Counter += 2;
        return context.Counter;
    }

    public int MoveDown(Context context)
    {
        if (context.Counter < Context.Limit)
        {
            context.State = new FastState();
            Console.WriteLine("||");
        }

        context.Counter -= 2;
        return context.Counter;
    }
}

class FastState : IState
{
    public int MoveUp(Context context)
    {
        context.Counter += 5;
        return context.Counter;
    }

    public int MoveDown(Context context)
    {
        if (context.Counter < Context.Limit)
        {
            context.State = new NormalState();
            Console.WriteLine("||");
        }

        context.Counter -= 5;
        return context.Counter;
    }
}

class Context
{
    public const int Limit = 10;
    public IState State { get; set; }
    public int Counter = Limit;

    public int Request(int n)
    {
        if (n == 2)
        {
            return State.MoveUp(this);
        }
        else
        {
            return State.MoveDown(this);
        }
    }
}