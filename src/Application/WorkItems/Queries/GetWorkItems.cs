using Domain.Primitives;

namespace Application.WorkItems.Queries;

public record GetWorkItemsQuery : IRequest<IEnumerable<WorkItem>>;

public class GetWorkItemsQueryHandler
    : IRequestHandler<GetWorkItemsQuery, IEnumerable<WorkItem>>
{
    private readonly IRepository<WorkItem> _repository;

    public GetWorkItemsQueryHandler(IRepository<WorkItem> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WorkItem>> Handle(
        GetWorkItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}