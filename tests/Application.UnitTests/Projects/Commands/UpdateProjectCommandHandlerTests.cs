using Application.Projects.Commands;
using Application.Projects.Requests;
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
public class UpdateProjectCommandHandlerTests
{
    private Mock<IRepository<Project>> _repositoryMock;
    private UpdateProjectCommandHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<Project>>();
        _handler = new UpdateProjectCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenValidProjectId_WhenHandlingUpdateCommand_ThenShouldUpdateProject()
    {
        // Arrange
        var existingProject = new Project { Id = 1, Title = "Old Title" };
        var updateRequest = new UpdateProjectRequest { Id = 1, Title = "New Title" };
        var command = new UpdateProjectCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingProject);

        // Act
        await ((IRequestHandler<UpdateProjectCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        existingProject.Title.Should().Be(updateRequest.Title);
        _repositoryMock.Verify(r => r.UpdateAsync(existingProject, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GivenInvalidProjectId_WhenHandlingUpdateCommand_ThenShouldThrowException()
    {
        // Arrange
        var updateRequest = new UpdateProjectRequest { Id = 99 };
        var command = new UpdateProjectCommand(updateRequest);

        _repositoryMock.Setup(r => r.GetByIdAsync(updateRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project)null);

        // Act
        Func<Task> act = async () => await ((IRequestHandler<UpdateProjectCommand>)_handler).Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}