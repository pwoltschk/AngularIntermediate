using Application.Common.Services;
using MediatR.Pipeline;

namespace Application.Common.Behaviours;

public class RequestLoggingBehaviour<TRequest>(ILogger logger, IUserContext currentUser)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = currentUser.UserId;

        logger.Information("Request: {@RequestName} | UserId: {UserId} | RequestData: {@Request}",
            requestName, userId, request);
        return Task.CompletedTask;
    }
}