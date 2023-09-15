namespace Application.WorkItems.Queries;

public record GetWorkItemsQuery : IRequest<IEnumerable<WorkItem>>;

public class GetWorkItemsQueryHandler
    : IRequestHandler<GetWorkItemsQuery, IEnumerable<WorkItem>>
{
    private readonly IApplicationDbContext _context;

    public GetWorkItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkItem>> Handle(
        GetWorkItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.WorkItems
            .ToListAsync(cancellationToken);
    }
}