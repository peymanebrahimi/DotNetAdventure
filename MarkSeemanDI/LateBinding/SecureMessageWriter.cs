using System;
using System.Security.Principal;
using Interfaces;

namespace LateBinding
{
    public class SecureMessageWriter : IMessageWriter
    {
        private readonly IMessageWriter writer;
        private readonly IIdentity identity;
        public SecureMessageWriter(
            IMessageWriter writer,
            IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException(nameof(identity));
            this.identity = identity;
            this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }
        public void Write(string message)
        {
            if (this.identity.IsAuthenticated)
            {
                this.writer.Write(message);
            }
        }
    }
}