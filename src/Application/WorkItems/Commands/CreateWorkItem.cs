using Application.Common;
using Application.WorkItems.Requests;
using Domain.Entities;
using MediatR;

namespace Application.WorkItems.Commands
{

    public record CreateWorkItemCommand(CreateWorkItemRequest Item) : IRequest<int>;

    public class CreateWorkItemCommandHandler
        : IRequestHandler<CreateWorkItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateWorkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateWorkItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new WorkItem
            {
                ProjectId = request.Item.ProjectId,
                Title = request.Item.Title,
            };

            _context.WorkItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
