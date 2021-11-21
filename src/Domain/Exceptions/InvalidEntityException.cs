using System;
using System.Diagnostics.CodeAnalysis;

namespace DistanceCalculator.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message)
            : base(message)
        {
        }
    }
}
