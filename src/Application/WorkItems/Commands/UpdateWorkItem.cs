using Application.Common;
using Application.WorkItems.Requests;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkItems.Commands
{
    public record UpdateWorkItemCommand(UpdateWorkItemRequest Item) : IRequest;

    public class UpdateWorkItemCommandHandler
        : AsyncRequestHandler<UpdateWorkItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateWorkItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(UpdateWorkItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.WorkItems.FirstOrDefaultAsync(
                i => i.Id == request.Item.Id, cancellationToken);

            if (entity == null)
            {
                throw new Exception($"The request ID {request.Item.Id} was not found.");
            }

            entity!.ProjectId = request.Item.ProjectId;
            entity.Title = request.Item.Title;
            entity.AssignedTo = request.Item.AssignedTo;
            entity.Iteration = request.Item.Iteration;
            entity.Priority = (Priority)request.Item.Priority;
            entity.Description = request.Item.Description;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
