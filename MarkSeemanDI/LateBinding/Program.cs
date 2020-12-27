using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Interfaces;
using Microsoft.Extensions.Configuration;

namespace LateBinding
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var here = Directory.GetCurrentDirectory();
            
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string typeName = configuration["messageWriter"];
            //Type type = Type.GetType(typeName, throwOnError: true);
            Type type1 = Type.GetType(typeName);


            string assemblyQualifiedName = Assembly.CreateQualifiedName("ThirdPartyLib", 
                "ThirdPartyLib.ConsoleMessageWriter");
            Type type2 = Type.GetType(assemblyQualifiedName);

            var alc = AssemblyLoadContext.Default;
            var alcAssemblies = alc.Assemblies;

            Assembly assembly = Assembly.LoadFile(Path.Combine(here, "ThirdPartyLib"));
            Type type3 = assembly.GetType("ThirdPartyLib.ConsoleMessageWriter");


            IMessageWriter writer = (IMessageWriter)Activator.CreateInstance(type1);


            writer.Write("Hello DI!");

            Console.ReadLine();
        }
    }
}
