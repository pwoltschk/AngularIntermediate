using Application.Common.Services;
using MediatR.Pipeline;


namespace YourAppNamespace.Behaviours
{
    public class RequestLoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        private readonly ILogger _logger;
        private readonly IUserInfo _currentUser;

        public RequestLoggingBehaviour(ILogger logger, IUserInfo currentUser)
        {
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId ?? string.Empty;
            var userName = $"{_currentUser.FirstName} {_currentUser.LastName}";

            _logger.Information("Request: {@RequestName} | UserId: {UserId} | UserName: {UserName} | RequestData: {@Request}",
                requestName, userId, userName, request);
        }
    }
}
