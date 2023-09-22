using Application.Common.Services;
using Domain.Events;
using Domain.Primitives;

namespace Application.WorkItems.Events
{
    internal class WorkItemAssignedDomainEventHandler : INotificationHandler<WorkItemAssignedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IRepository<WorkItem> _repository;

        public WorkItemAssignedDomainEventHandler(
            IEmailService emailService,
            IRepository<WorkItem> repository)
        {
            _emailService = emailService;
            _repository = repository;
        }
        public async Task Handle(WorkItemAssignedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var workItem = await _repository
                .GetByIdAsync(domainEvent.id);

            if (workItem is null) return;

            // Replcace with real implementation
            await _emailService.SendEmailAsync("You are assigned to this workitem","Example Subject", workItem.AssignedTo );
        }
    }
}