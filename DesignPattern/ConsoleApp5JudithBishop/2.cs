namespace ConsoleApp5JudithBishop;

// State
public abstract class TrafficLightState
{
    public abstract void Handle(TrafficLight context);
}

// ConcreteState
public class RedLight : TrafficLightState
{
    public override void Handle(TrafficLight context)
    {
        Console.WriteLine("Red light - Stop!");
        context.SetState(new GreenLight());
    }
}
public class GreenLight : TrafficLightState
{
    public override void Handle(TrafficLight context)
    {
        Console.WriteLine("Green light - Go!");
        context.SetState(new RedLight());
    }
}
// Context
public class TrafficLight
{
    private TrafficLightState _state;
    public TrafficLight(TrafficLightState initialState)
    {
        _state = initialState;
    }
    public void SetState(TrafficLightState state)
    {
        _state = state;
    }
    public void Change()
    {
        _state.Handle(this);
    }
}