using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Models;
using DistanceCalculator.Application.Common.Services;

namespace DistanceCalculator.Application.Common.Interfaces
{
    public interface IIdentity : IService
    {
        Task<string> GetUserName(string userId);

        Task<(Result Result, string UserId)> CreateUser(string userName, string password);

        Task<Result> DeleteUser(string userId);
    }
}
