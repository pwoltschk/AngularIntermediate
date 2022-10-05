namespace Application.Projects.Commands;

public record DeleteProjectCommand(int Id) : IRequest;

public class DeleteProjectCommandHandler
    : AsyncRequestHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new Exception($"The request ID {request.Id} was not found.");
        }

        _context.Projects.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}