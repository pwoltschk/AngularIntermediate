namespace Application.Projects.Queries;

public record GetProjectsQuery : IRequest<IEnumerable<Project>>;

public class GetProjectsQueryHandler
    : IRequestHandler<GetProjectsQuery, IEnumerable<Project>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(p => p.WorkItems)
            .ToListAsync(cancellationToken);

    }
}