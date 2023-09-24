using Application.WorkItems.Commands;
using Application.WorkItems.Requests;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Commands
{
    [TestClass]
    public class CreateWorkItemCommandHandlerTests
    {
        private Mock<IRepository<WorkItem>> _repositoryMock;
        private CreateWorkItemCommandHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<WorkItem>>();
            _handler = new CreateWorkItemCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task GivenValidCreateWorkItemRequest_WhenHandling_ThenShouldAddWorkItemAndReturnId()
        {
            // Arrange
            var createRequest = new CreateWorkItemRequest
            {
                ProjectId = 1,
                Title = "New WorkItem"
            };
            var command = new CreateWorkItemCommand(createRequest);
            var newWorkItem = new WorkItem { Id = 10, ProjectId = createRequest.ProjectId, Title = createRequest.Title };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<WorkItem>(), It.IsAny<CancellationToken>()))
                .Callback<WorkItem, CancellationToken>((w, ct) => w.Id = 10);

            // Act
            var result = await ((IRequestHandler<CreateWorkItemCommand, int>)_handler)
                .Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(10);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<WorkItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}