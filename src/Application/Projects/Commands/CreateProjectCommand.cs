using Application.Projects.Requests;
using Domain.Primitives;

namespace Application.Projects.Commands;

public record CreateProjectCommand(CreateProjectRequest Project) : IRequest<int>;

public class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IRepository<Project> _repository;

    public CreateProjectCommandHandler(IRepository<Project> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Project
        {
            Title = request.Project.Title
        };

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}