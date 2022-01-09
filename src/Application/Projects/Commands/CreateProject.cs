using Application.Common;
using Application.Projects.Requests;
using Domain.Entities;
using MediatR;

namespace Application.Projects.Commands;

public record CreateProjectCommand(CreateProjectRequest Project) : IRequest<int>;

public class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Project();

        entity.Title = request.Project.Title;

        _context.Projects.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}