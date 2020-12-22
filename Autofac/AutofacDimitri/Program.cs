using System;
using System.Collections.Generic;
using Autofac;

namespace AutofacDimitri
{
    class Program
    {
        static void Main(string[] args)
        {
            //Registration_Concepts.RegisteringTypes3();
            //Registration_Concepts.DefaultRegistrations4();
            //Registration_Concepts.ChoiceOfConstructor5();
            //Registration_Concepts.RegisteringInstances6();
            //Registration_Concepts.LambdaExpressionComponents7();
            //Registration_Concepts.OpenGenericComponents8();


            DelayedInstantiation.Run();


            Console.ReadLine();
        }



    }
}
