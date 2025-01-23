// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

/*
 I have a c# coding problem which some tasks are common and some tasks are specifics and result of each step or task is passed to the next task. I want a sample code base on good practices and design patterns applied


Show drafts


Understanding the Problem:

Before we dive into the code, let's break down the problem and identify potential design patterns that could be applied:

Common Tasks: These are tasks that are shared across different specific tasks.
Specific Tasks: These are tasks that are unique to a particular workflow.
Dependency Chain: The output of one task becomes the input for the next.
Potential Design Patterns:

Template Method: This pattern can be used to define the overall structure of the workflow, while allowing subclasses to implement specific steps.
Chain of Responsibility: This pattern can be used to handle the dependency chain, where each task in the chain can process the request or pass it on to the next task.
Pipeline: This pattern can be used to model a series of processing stages, where each stage takes an input, processes it, and produces an output.
Sample Code:

Here's a C# code example using the Template Method and Chain of Responsibility patterns to illustrate the concept:
*/


public interface ITask
{
    void Execute(object input);
}

public abstract class TaskBase : ITask
{
    protected ITask NextTask { get; set; }

    public abstract void Execute(object input);

    protected void SetNextTask(ITask nextTask)
    {
        NextTask = nextTask;
    }
}

public class CommonTask1 : TaskBase
{
    public override void Execute(object input)
    {
        // Common task 1 logic
        var output = ProcessInput(input);
        NextTask?.Execute(output);
    }

    private object ProcessInput(object input)
    {
        // ... implementation ...
        return processedInput;
    }
}

public class SpecificTaskA : TaskBase
{
    public override void Execute(object input)
    {
        // Specific task A logic
        var output = ProcessInputA(input);
        NextTask?.Execute(output);
    }

    private object ProcessInputA(object input)
    {
        // ... implementation ...
        return processedInputA;
    }
}

// ... other specific tasks ...

public class Workflow
{
    private readonly ITask _initialTask;

    public Workflow(ITask initialTask)
    {
        _initialTask = initialTask;
    }

    public void Start(object initialInput)
    {
        _initialTask.Execute(initialInput);
    }
}