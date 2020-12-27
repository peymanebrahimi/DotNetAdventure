using System;
using Interfaces;

namespace LateBinding
{
    public class Salutation
    {
        private readonly IMessageWriter writer;
        public Salutation(IMessageWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            this.writer = writer;
        }
        public void Exclaim()
        {
            this.writer.Write("Hello DI!");
        }
    }
}