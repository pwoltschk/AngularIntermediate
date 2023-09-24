using Application.Projects.Commands;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Projects.Commands;

[TestClass]
public class DeleteProjectCommandHandlerTests
{
    private Mock<IRepository<Project>> _repositoryMock;
    private DeleteProjectCommandHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<Project>>();
        _handler = new DeleteProjectCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenValidProjectId_WhenHandlingDeleteCommand_ThenShouldRemoveProject()
    {
        // Arrange
        var validProjectId = 1;
        var project = new Project { Id = validProjectId };
        var command = new DeleteProjectCommand(validProjectId);

        _repositoryMock.Setup(r => r.GetByIdAsync(validProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        // Act
        await ((IRequestHandler<DeleteProjectCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.RemoveAsync(project, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GivenInvalidProjectId_WhenHandlingDeleteCommand_ThenShouldThrowException()
    {
        // Arrange
        var invalidProjectId = 99;
        var command = new DeleteProjectCommand(invalidProjectId);

        _repositoryMock.Setup(r => r.GetByIdAsync(invalidProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project)null);

        // Act
        Func<Task> act = async () => await ((IRequestHandler<DeleteProjectCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}