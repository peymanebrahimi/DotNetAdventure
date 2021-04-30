using System.Collections.Concurrent;

namespace ThreadingDmitri.ConsoleApp1
{
    // ConcurrentDictionary
    public class Example1
    {
        static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void DoMain()
        {
            capitals["Russia"] = "leningrad";

            capitals.AddOrUpdate("Russia", "Moscow", UpdateValueFactory);
        }

        private static string UpdateValueFactory(string arg1, string arg2)
        {
            var result = arg1 + arg2;
            return result;
        }
    }

}