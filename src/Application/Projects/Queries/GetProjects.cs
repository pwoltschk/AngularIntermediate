using Domain.Primitives;

namespace Application.Projects.Queries;

public record GetProjectsQuery : IRequest<IEnumerable<Project>>;

public class GetProjectsQueryHandler(IRepository<Project> repository)
    : IRequestHandler<GetProjectsQuery, IEnumerable<Project>>
{
    public async Task<IEnumerable<Project>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);

    }
}