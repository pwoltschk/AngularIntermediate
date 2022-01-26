using Application.Common;
using Application.Projects.Requests;
using MediatR;

namespace Application.Projects.Commands;

public record UpdateProjectCommand(UpdateProjectRequest Project) : IRequest;

public class UpdateProjectCommandHandler
    : AsyncRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects.FindAsync(request.Project.Id);

        if (entity == null)
        {
            throw new Exception($"The request ID {entity.Id} was not found.");
        }

        entity.Title = request.Project.Title;

        await _context.SaveChangesAsync(cancellationToken);
    }
}