using System;

namespace prayzzz.Common.Mapping
{
    public class InvalidContextException : Exception
    {
        public InvalidContextException(string message) : base(message)
        {
        }
    }
}