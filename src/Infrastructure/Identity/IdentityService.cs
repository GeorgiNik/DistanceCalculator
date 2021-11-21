using System.Linq;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Infrastructure.Identity
{
    public class IdentityService : IIdentity
    {
        private readonly UserManager<User> _userManager;

        public IdentityService(UserManager<User> userManager)
            => this._userManager = userManager;

        public async Task<string> GetUserName(string userId)
            => await this._userManager
                .Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

        public async Task<(Result Result, string UserId)> CreateUser(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = userName,
            };

            var result = await this._userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUser(string userId)
        {
            var user = this._userManager
                .Users
                .SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await this.DeleteUser(user);
            }

            return Result.Success;
        }

        public async Task<Result> DeleteUser(User user)
        {
            var result = await this._userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
