using Application.Projects.Requests;
using Domain.Primitives;

namespace Application.Projects.Commands;

public record CreateProjectCommand(CreateProjectRequest Project) : IRequest<int>;

public class CreateProjectCommandHandler(IRepository<Project> repository) : IRequestHandler<CreateProjectCommand, int>
{
    public async Task<int> Handle(CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Project
        {
            Title = request.Project.Title
        };

        await repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}