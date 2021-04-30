using System;
using System.Threading.Tasks;

namespace ThreadingDmitri.ConsoleApp1
{
    // 5. Task Coordination
    public class Example2
    {
        public static void DoContinuation()
        {
            var task = Task.Factory.StartNew(() => "Task1");
            var task2 = Task.Run(() => "Task2");

            var task3 = Task.WhenAll(task, task2)
                .ContinueWith(tasks =>
                {
                    Console.WriteLine("\tTasks Completed.");
                    foreach (var t in tasks.Result)
                    {
                        Console.WriteLine($"\t-{t}");
                    }

                    Console.WriteLine("\tAll Done.");
                });

            task3.Wait();
        }

        public static void DoChildTask()
        {
            var parent = new Task(() =>
            {
                // detached = just a subtask within a task
                // no relationship

                // attached

                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting...");
                    Task.Delay(3000).Wait();
                    Console.WriteLine("Child task finished.");

                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Unfortunately, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                child.Start();

                Console.WriteLine("Parent task starting...");
                Task.Delay(1000).Wait();
                Console.WriteLine("Parent task finished.");
            });

            parent.Start();
            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }

}