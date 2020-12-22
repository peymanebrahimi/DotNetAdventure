using System;
using System.Collections.Generic;
using Autofac;

namespace AutofacDimitri
{
    public interface ILog
    {
        void Write(string message);
    }

    public interface IConsole { }

    public class ConsoleLog : ILog, IConsole
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class EmailLog : ILog
    {
        private const string adminEmail = "admin@foo.com";

        public void Write(string message)
        {
            Console.WriteLine($"Email sent to {adminEmail} : {message}");
        }
    }

    public class Engine
    {
        private ILog log;
        private int id;

        public Engine(ILog log)
        {
            this.log = log;
            id = new Random().Next();
        }

        public Engine(ILog log, int id)
        {
            this.log = log;
            this.id = id;
        }

        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }

    public class Car
    {
        private Engine engine;
        private ILog log;

        public Car(Engine engine, ILog log)
        {
            this.engine = engine;
            this.log = log;
        }

        public Car(Engine engine)
        {
            this.engine = engine;
            this.log = new EmailLog();
        }

        public void Go()
        {
            engine.Ahead(100);
            log.Write("Car going forward...");
        }
    }


    class Registration_Concepts
    {
        public static void RegisteringTypes3()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var log = container.Resolve<ConsoleLog>(); // is possible, because of AsSelf()

            var car = container.Resolve<Car>();
            car.Go();
        }

        public static void DefaultRegistrations4()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EmailLog>()
                .As<ILog>();

            builder.RegisterType<ConsoleLog>()
                .As<ILog>()
                .As<IConsole>()
                .PreserveExistingDefaults();

            builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var car = container.Resolve<Car>();
            car.Go();
        }

        public static void ChoiceOfConstructor5()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.RegisterType<Engine>();
            builder.RegisterType<Car>()
                .UsingConstructor(typeof(Engine));

            IContainer container = builder.Build();

            var car = container.Resolve<Car>();
            car.Go();
        }

        public static void RegisteringInstances6()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<ConsoleLog>().As<ILog>();

            var log = new ConsoleLog();
            builder.RegisterInstance(log).As<ILog>();

            builder.RegisterType<Engine>();
            builder.RegisterType<Car>()
                .UsingConstructor(typeof(Engine));

            IContainer container = builder.Build();

            var car = container.Resolve<Car>();
            car.Go();
        }

        public static void LambdaExpressionComponents7()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();

            builder.Register((IComponentContext c) =>
                new Engine(c.Resolve<ILog>(), 123));

            //builder.RegisterType<Engine>();
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var car = container.Resolve<Car>();
            car.Go();
        }

        public static void OpenGenericComponents8()
        {
            var builder = new ContainerBuilder();

            // IList<T> --> List<T>
            // IList<int> --> List<int>
            builder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>));

            IContainer container = builder.Build();

            var myList = container.Resolve<IList<int>>();
            Console.WriteLine(myList.GetType());
        }
    }
}
