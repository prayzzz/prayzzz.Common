using System;
using System.Runtime.Serialization;

namespace prayzzz.Common.Mapping
{
    public class InvalidContextException : Exception
    {
        public InvalidContextException(string message) : base(message)
        {
        }

        public InvalidContextException() : base()
        {
        }

        protected InvalidContextException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}