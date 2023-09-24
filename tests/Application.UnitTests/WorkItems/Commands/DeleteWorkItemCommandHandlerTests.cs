using Application.WorkItems.Commands;
using Domain.Entities;
using Domain.Primitives;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Commands;

[TestClass]
public class DeleteWorkItemCommandHandlerTests
{
    private Mock<IRepository<WorkItem>> _repositoryMock;
    private DeleteWorkItemCommandHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<WorkItem>>();
        _handler = new DeleteWorkItemCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenValidWorkItemId_WhenHandlingDeleteCommand_ThenShouldRemoveWorkItem()
    {
        // Arrange
        var validWorkItemId = 1;
        var workItem = new WorkItem();
        var command = new DeleteWorkItemCommand(validWorkItemId);

        _repositoryMock.Setup(r => r.GetByIdAsync(validWorkItemId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(workItem);

        // Act
        await ((IRequestHandler<DeleteWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.RemoveAsync(workItem, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GivenInvalidWorkItemId_WhenHandlingDeleteCommand_ThenShouldNotRemoveAnything()
    {
        // Arrange
        var invalidWorkItemId = 99;
        var command = new DeleteWorkItemCommand(invalidWorkItemId);

        _repositoryMock.Setup(r => r.GetByIdAsync(invalidWorkItemId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((WorkItem)null);

        // Act
        await ((IRequestHandler<DeleteWorkItemCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.RemoveAsync(It.IsAny<WorkItem>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}