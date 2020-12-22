using System;

namespace AutofacDimitri
{
    class Animal
    {
        public string Name { get; set; }
    }

    class Bear : Animal
    {

        public int Honey { get; set; }
    }

    class Inheritance
    {
        public static void Run()
        {
            Animal a = new Animal() { Name = "Animal" };
            Bear b = new Bear() { Name = "Bear", Honey = 10 };
            a = b;
            //b = a;
            Console.WriteLine(a.Name);
        }
    }
}
