namespace Application.Common.Behaviours;

public sealed class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            logger.Information(ex, "Exception Request: Unhandled Exception for Request {@requestName} {@request}", requestName, request);

            throw;
        }
    }
}