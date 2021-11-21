using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUser _currentUserService;
        private readonly IIdentity _identityService;

        public RequestLogger(
            ILogger<RequestLogger<TRequest>> logger,
            ICurrentUser currentUserService,
            IIdentity identityService)
        {
            this._logger = logger;
            this._currentUserService = currentUserService;
            this._identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = this._currentUserService.UserId;
            var userName = await this._identityService.GetUserName(userId);

            this._logger.LogInformation(
                "DistanceCalculator Request: {StartLocationName} {@UserId} {@UserName} {@Request}",
                requestName,
                userId,
                userName ?? "Anonymous",
                request);
        }
    }
}
