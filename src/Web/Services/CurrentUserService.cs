using System.Security.Claims;
using DistanceCalculator.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DistanceCalculator.Web.Services
{
    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
            => this.UserId = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

        public string UserId { get; }
    }
}
