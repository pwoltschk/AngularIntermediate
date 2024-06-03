using Application.Projects.Requests;
using Domain.Primitives;

namespace Application.Projects.Commands;

public record UpdateProjectCommand(UpdateProjectRequest Project) : IRequest;

public class UpdateProjectCommandHandler(IRepository<Project> repository) : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Project.Id, cancellationToken) ?? throw new Exception($"The request ID {request.Project.Id} was not found.");
        entity.Title = request.Project.Title;

        await repository.UpdateAsync(entity, cancellationToken);
    }
}