using Application.WorkItems.Commands;
using Application.WorkItems.Requests;
using Domain.Entities;
using Domain.Primitives;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Commands;

[TestClass]
public class UpdateWorkItemCommandHandlerTests
{
    [TestMethod]
    public async Task GivenValidRequest_WhenItemExists_ThenShouldUpdateWorkItem()
    {
        var repositoryMock = new Mock<IRepository<WorkItem>>();
        var workItem = new WorkItem { Id = 0, AssignedTo = "OldUser" };

        var updateRequest = new UpdateWorkItemRequest
        {
            Id = workItem.Id,
            ProjectId = 0,
            Title = "New Title",
            AssignedTo = "NewUser",
            Iteration = "Sprint 1",
            Priority = 2,
            Description = "New Description",
            Stage = 1
        };

        repositoryMock.Setup(r => r.GetByIdAsync(workItem.Id)).ReturnsAsync(workItem);

        var handler = new TestableUpdateWorkItemCommandHandler(repositoryMock.Object);

        await handler.TestHandle(new UpdateWorkItemCommand(updateRequest), CancellationToken.None);

        repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<WorkItem>()), Times.Once);
        workItem.Title.Should().Be("New Title");
        workItem.AssignedTo.Should().Be("NewUser");
        workItem.Priority.Should().Be(Priority.FromLevel(2));
        workItem.Stage.Should().Be(Stage.FromId(1));
    }

    [TestMethod]
    public async Task GivenInvalidRequest_WhenItemDoesNotExist_ThenShouldThrowException()
    {
        var repositoryMock = new Mock<IRepository<WorkItem>>();
        var updateRequest = new UpdateWorkItemRequest { Id = 0 };

        repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id)).ReturnsAsync((WorkItem)null);

        var handler = new TestableUpdateWorkItemCommandHandler(repositoryMock.Object);

        Func<Task> act = async () => await handler.TestHandle(new UpdateWorkItemCommand(updateRequest), CancellationToken.None);
        await act.Should().ThrowAsync<Exception>();
    }

    [TestMethod]
    public async Task GivenValidRequest_WhenWorkItemAssignedToDifferentUser_ThenShouldAddWorkItemAssignedEvent()
    {
        var repositoryMock = new Mock<IRepository<WorkItem>>();
        var workItem = new WorkItem { Id = 0, AssignedTo = "OldUser" };

        var updateRequest = new UpdateWorkItemRequest
        {
            Id = workItem.Id,
            AssignedTo = "NewUser"
        };

        repositoryMock.Setup(r => r.GetByIdAsync(workItem.Id)).ReturnsAsync(workItem);

        var handler = new TestableUpdateWorkItemCommandHandler(repositoryMock.Object);

        await handler.TestHandle(new UpdateWorkItemCommand(updateRequest), CancellationToken.None);

        workItem.DomainEvents.Count.Should().Be(1);
    }

    [TestMethod]
    public async Task GivenValidRequest_WhenWorkItemAssignedToSameUser_ThenShouldNotAddWorkItemAssignedEvent()
    {
        var repositoryMock = new Mock<IRepository<WorkItem>>();
        var workItem = new WorkItem { Id = 0, AssignedTo = "SameUser" };

        var updateRequest = new UpdateWorkItemRequest
        {
            Id = workItem.Id,
            AssignedTo = "SameUser"
        };

        repositoryMock.Setup(r => r.GetByIdAsync(workItem.Id)).ReturnsAsync(workItem);

        var handler = new TestableUpdateWorkItemCommandHandler(repositoryMock.Object);

        await handler.TestHandle(new UpdateWorkItemCommand(updateRequest), CancellationToken.None);

        workItem.DomainEvents.Should().BeEmpty();
    }
}
public class TestableUpdateWorkItemCommandHandler : UpdateWorkItemCommandHandler
{
    public TestableUpdateWorkItemCommandHandler(IRepository<WorkItem> repository)
        : base(repository) { }

    public async Task TestHandle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
    {
        await Handle(request, cancellationToken);
    }
}