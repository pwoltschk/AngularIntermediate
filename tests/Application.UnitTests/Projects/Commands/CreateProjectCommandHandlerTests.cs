using Application.Projects.Commands;
using Application.Projects.Requests;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Projects.Commands
{
    [TestClass]
    public class CreateProjectCommandHandlerTests
    {
        private Mock<IRepository<Project>> _repositoryMock;
        private CreateProjectCommandHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<Project>>();
            _handler = new CreateProjectCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task GivenValidCreateProjectRequest_WhenHandling_ThenShouldAddProjectAndReturnId()
        {
            // Arrange
            var createRequest = new CreateProjectRequest
            {
                Title = "New Project"
            };
            var command = new CreateProjectCommand(createRequest);
            var newProject = new Project { Id = 1, Title = createRequest.Title };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
                .Callback<Project, CancellationToken>((p, ct) => p.Id = 1);

            // Act
            var result = await ((IRequestHandler<CreateProjectCommand, int>)_handler)
                .Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(1);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}