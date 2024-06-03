using Application.Common.Services;
using Domain.Events;
using Domain.Primitives;

namespace Application.WorkItems.Events;

internal class WorkItemAssignedDomainEventHandler(
    IEmailService emailService,
    IRepository<WorkItem> repository)
    : INotificationHandler<WorkItemAssignedDomainEvent>
{
    public async Task Handle(WorkItemAssignedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var workItem = await repository
            .GetByIdAsync(domainEvent.Id, cancellationToken);

        if (workItem is null) return;

        // replace with real implementation
        await emailService.SendEmailAsync("You are assigned to this workitem", "Example Subject", workItem.AssignedTo);
    }
}