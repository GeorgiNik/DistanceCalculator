using System;
using System.Diagnostics.CodeAnalysis;

namespace DistanceCalculator.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InvalidLocationException : Exception
    {
        public InvalidLocationException(string message)
            : base(message)
        {
        }
    }
}
