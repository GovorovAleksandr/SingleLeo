using System;

namespace GovorovAleksandr.SingleLeo
{
    public class SingletonInvariantException : Exception
    {
        internal SingletonInvariantException(string message) : base(message) {}

        internal SingletonInvariantException(string message, Exception innerException) : base(message, innerException) {}
    }
}