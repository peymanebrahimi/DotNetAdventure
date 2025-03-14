﻿namespace StateDesignPattern;

/*
 * Let’s consider the classic example of an order processing system
 * where an order can be in various states,
 * such as “New”, “Processed”, “Shipped”, “Delivered”, and “Canceled”.
 * Each state will have different actions that can be performed
 * and will transition to different states based on those actions.
 * Here’s how the State Design Pattern can be applied:
 */

// Context
public class Order
{
    private IOrderState _currentState = new New();
    public void SetState(IOrderState state) => _currentState = state;
    public void NextState() => _currentState.Next(this);
    public void PrevState() => _currentState.Prev(this);
    public void PrintStatus() => _currentState.PrintStatus();
}

// State Interface
public interface IOrderState
{
    void Next(Order order);
    void Prev(Order order);
    void PrintStatus();
}

// Concrete State Classes
public class New : IOrderState
{
    public void Next(Order order) => order.SetState(new Processed());
    public void Prev(Order order) => Console.WriteLine("The order is in its new state.");
    public void PrintStatus() => Console.WriteLine("Order is in NEW state.");
}

public class Processed : IOrderState
{
    public void Next(Order order) => order.SetState(new Shipped());
    public void Prev(Order order) => order.SetState(new New());
    public void PrintStatus() => Console.WriteLine("Order is PROCESSED.");
}

public class Shipped : IOrderState
{
    public void Next(Order order) => order.SetState(new Delivered());
    public void Prev(Order order) => order.SetState(new Processed());
    public void PrintStatus() => Console.WriteLine("Order is SHIPPED.");
}

public class Delivered : IOrderState
{
    public void Next(Order order) => Console.WriteLine("Order is already delivered.");
    public void Prev(Order order) => order.SetState(new Shipped());
    public void PrintStatus() => Console.WriteLine("Order is DELIVERED.");
}

public class Canceled : IOrderState
{
    public void Next(Order order) => Console.WriteLine("Order is canceled and cannot proceed.");
    public void Prev(Order order) => Console.WriteLine("Order is canceled and cannot go back.");
    public void PrintStatus() => Console.WriteLine("Order is CANCELED.");
}

// Test the State Pattern
public class Program
{
    public static void Main(string[] args)
    {
        Order order = new Order();
        order.PrintStatus(); // Output: Order is in NEW state.
        order.NextState();
        order.PrintStatus(); // Output: Order is PROCESSED.
        order.NextState();
        order.PrintStatus(); // Output: Order is SHIPPED.
        order.PrevState();
        order.PrintStatus(); // Output: Order is PROCESSED.
        order.SetState(new Canceled());
        order.PrintStatus(); // Output: Order is CANCELED.
        Console.ReadKey();
    }
}