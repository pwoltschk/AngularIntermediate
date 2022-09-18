namespace Application.WorkItems.Commands
{
    public record DeleteWorkItemCommand(int Id) : IRequest;

    public class DeleteWorkItemCommandHandler
        : AsyncRequestHandler<DeleteWorkItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteWorkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteWorkItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.WorkItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new Exception($"The request ID {request.Id} was not found.");
            }

            _context.WorkItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
