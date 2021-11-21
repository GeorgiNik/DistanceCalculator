using System.Linq;
using DistanceCalculator.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace DistanceCalculator.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result) 
            => result.Succeeded
                ? Result.Success
                : Result.Failure(result.Errors.Select(e => e.Description));
    }
}