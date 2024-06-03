using Domain.Primitives;

namespace Application.WorkItems.Queries;

public record GetWorkItemsQuery : IRequest<IEnumerable<WorkItem>>;

public class GetWorkItemsQueryHandler(IRepository<WorkItem> repository)
    : IRequestHandler<GetWorkItemsQuery, IEnumerable<WorkItem>>
{
    public async Task<IEnumerable<WorkItem>> Handle(
        GetWorkItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
}