using Application.Common.Services;
using MediatR.Pipeline;

namespace Application.Common.Behaviours;

public class RequestLoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly IUserContext _currentUser;

    public RequestLoggingBehaviour(ILogger logger, IUserContext currentUser)
    {
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.UserId;

        _logger.Information("Request: {@RequestName} | UserId: {UserId} | RequestData: {@Request}",
            requestName, userId, request);
    }
}