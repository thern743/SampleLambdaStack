using System;

namespace Lambda.Common.Exceptions
{
    public class NullResponseException : Exception
    {
        public NullResponseException()  { }
        public NullResponseException(string message) : base(message) { }
    }
}
