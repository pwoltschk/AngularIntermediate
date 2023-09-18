using Domain.Primitives;

namespace Application.Projects.Commands;

public record DeleteProjectCommand(int Id) : IRequest;

public class DeleteProjectCommandHandler
    : AsyncRequestHandler<DeleteProjectCommand>
{
    private readonly IRepository<Project> _repository;

    public DeleteProjectCommandHandler(IRepository<Project> repository)
    {
        _repository = repository;
    }

    protected override async Task Handle(DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id) ?? throw new Exception($"The request ID {request.Id} was not found.");
        await _repository.RemoveAsync(entity);
    }
}