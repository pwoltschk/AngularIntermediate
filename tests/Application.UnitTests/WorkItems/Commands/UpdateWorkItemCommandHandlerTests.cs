using Application.WorkItems.Commands;
using Application.WorkItems.Requests;
using Domain.Entities;
using Domain.Events;
using Domain.Primitives;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Commands;

[TestClass]
public class UpdateWorkItemCommandHandlerTests
{
    private Mock<IRepository<WorkItem>> _repositoryMock;
    private UpdateWorkItemCommandHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<WorkItem>>();
        _handler = new UpdateWorkItemCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenValidUpdateWorkItemRequest_WhenHandlingUpdateCommand_ThenShouldUpdateWorkItem()
    {
        // Arrange
        var existingWorkItem = new WorkItem { Id = 1, AssignedTo = "OldAssignee" };
        var updateRequest = new UpdateWorkItemRequest
        {
            Id = 1,
            ProjectId = 2,
            Title = "Updated Title",
            AssignedTo = "NewAssignee",
            Iteration = "Iteration1",
            Priority = 2,
            Description = "Updated Description",
            Stage = 1
        };
        var command = new UpdateWorkItemCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingWorkItem);

        // Act
        await ((IRequestHandler<UpdateWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        existingWorkItem.ProjectId.Should().Be(updateRequest.ProjectId);
        existingWorkItem.Title.Should().Be(updateRequest.Title);
        existingWorkItem.AssignedTo.Should().Be(updateRequest.AssignedTo);
        existingWorkItem.Priority.Level.Should().Be(updateRequest.Priority);
        existingWorkItem.Description.Should().Be(updateRequest.Description);
        existingWorkItem.Stage.Id.Should().Be(updateRequest.Stage);
        _repositoryMock.Verify(r => r.UpdateAsync(existingWorkItem, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GivenInvalidWorkItemId_WhenHandlingUpdateCommand_ThenShouldThrowException()
    {
        // Arrange
        var updateRequest = new UpdateWorkItemRequest { Id = 99 };
        var command = new UpdateWorkItemCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((WorkItem)null);

        // Act
        Func<Task> act = async () => await ((IRequestHandler<UpdateWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage($"The request ID {updateRequest.Id} was not found.");
    }

    [TestMethod]
    public async Task GivenWorkItemAssigneeChanged_WhenHandlingUpdateCommand_ThenShouldAddWorkItemAssignedDomainEvent()
    {
        // Arrange
        var existingWorkItem = new WorkItem { Id = 1, AssignedTo = "OldAssignee" };
        var updateRequest = new UpdateWorkItemRequest
        {
            Id = 1,
            AssignedTo = "NewAssignee"
        };
        var command = new UpdateWorkItemCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingWorkItem);

        // Act
        await ((IRequestHandler<UpdateWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        existingWorkItem.DomainEvents.Should().ContainSingle(e => e is WorkItemAssignedDomainEvent);
    }

    [TestMethod]
    public async Task GivenWorkItemAssigneeNotChanged_WhenHandlingUpdateCommand_ThenShouldNotAddWorkItemAssignedDomainEvent()
    {
        // Arrange
        var existingWorkItem = new WorkItem { Id = 1, AssignedTo = "SameAssignee" };
        var updateRequest = new UpdateWorkItemRequest
        {
            Id = 1,
            AssignedTo = "SameAssignee"
        };
        var command = new UpdateWorkItemCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingWorkItem);

        // Act
        await ((IRequestHandler<UpdateWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        existingWorkItem.DomainEvents.Should().BeEmpty();
    }
}