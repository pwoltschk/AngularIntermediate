using Domain.Primitives;

namespace Application.Projects.Queries;

public record GetProjectsQuery : IRequest<IEnumerable<Project>>;

public class GetProjectsQueryHandler
    : IRequestHandler<GetProjectsQuery, IEnumerable<Project>>
{
    private readonly IRepository<Project> _repository;

    public GetProjectsQueryHandler(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Project>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);

    }
}