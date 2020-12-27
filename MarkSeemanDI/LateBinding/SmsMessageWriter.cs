using System;
using Interfaces;

namespace LateBinding
{
    public class SmsMessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}