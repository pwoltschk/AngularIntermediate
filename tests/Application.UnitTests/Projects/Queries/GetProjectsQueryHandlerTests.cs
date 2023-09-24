using Application.Projects.Queries;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Projects.Queries
{
    [TestClass]
    public class GetProjectsQueryHandlerTests
    {
        private Mock<IRepository<Project>> _repositoryMock;
        private GetProjectsQueryHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<Project>>();
            _handler = new GetProjectsQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task GivenGetProjectsQuery_WhenHandling_ThenShouldReturnAllProjects()
        {
            // Arrange
            var projects = new List<Project> { new Project { Id = 1, Title = "Project 1" }, new Project { Id = 2, Title = "Project 2" } };
            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(projects);

            var query = new GetProjectsQuery();

            // Act
            var result = await ((IRequestHandler<GetProjectsQuery, IEnumerable<Project>>)_handler)
                .Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(projects);
            _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}