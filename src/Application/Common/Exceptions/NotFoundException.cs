using System;
using System.Diagnostics.CodeAnalysis;

namespace DistanceCalculator.Application.Common.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity '{name}' ({key}) was not found.")
        {
        }
    }
}
