using System;
using Autofac;

namespace AutofacDimitri
{
    interface ILog2 : IDisposable
    {
        void Write(string message);
    }

    class ConsoleLog2 : ILog2
    {
        public void Dispose()
        {
            Console.WriteLine($"{GetType()} is disposed.");
        }

        public void Write(string message)
        {
            Console.WriteLine($"{GetType().Name}.Write: {message}.");
        }
    }

    class SmsLog2 : ILog2
    {
        public void Dispose()
        {
            Console.WriteLine($"{GetType()} is disposed.");
        }

        public void Write(string message)
        {
            Console.WriteLine($"{GetType().Name}.Write: {message}.");
        }
    }

    class Reporting1
    {
        private readonly Lazy<ILog2> _log;

        public Reporting1(Lazy<ILog2> log)
        {
            _log = log;
            Console.WriteLine($"{GetType().Name} component created.");
        }

        public void Report()
        {
            _log.Value.Write($"{GetType().Name} reporting");
        }
    }

    class DelayedInstantiation
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<ConsoleLog2>().As<ILog2>();
            builder.RegisterType<SmsLog2>().As<ILog2>();
            builder.RegisterType<Reporting1>();

            using var container = builder.Build();
            var reporting1 = container.Resolve<Reporting1>();
            reporting1.Report();

        }
    }

}
