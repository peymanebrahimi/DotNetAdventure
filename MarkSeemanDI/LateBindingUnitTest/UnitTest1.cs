using System;
using Interfaces;
using LateBinding;
using Xunit;

namespace LateBindingUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void ExclaimWillWriteCorrectMessageToMessageWriter()
        {
            var writer = new SpyMessageWriter();
            var sut = new Salutation(writer);
            sut.Exclaim();
            Assert.Equal(
                expected: "Hello DI!",
                actual: writer.WrittenMessage);
        }
    }

    public class SpyMessageWriter : IMessageWriter
    {
        public string WrittenMessage { get; private set; }
        public void Write(string message)
        {
            this.WrittenMessage += message;
        }
    }
}
