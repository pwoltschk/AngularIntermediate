using Application.WorkItems.Queries;
using Domain.Entities;
using Domain.Primitives;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Queries;

[TestClass]
public class GetWorkItemsQueryHandlerTests
{
    private Mock<IRepository<WorkItem>> _repositoryMock;
    private GetWorkItemsQueryHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<WorkItem>>();
        _handler = new GetWorkItemsQueryHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenGetWorkItemsQuery_WhenHandling_ThenShouldReturnWorkItems()
    {
        // Arrange
        var workItems = new List<WorkItem> { new WorkItem(), new WorkItem() };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(workItems);

        var query = new GetWorkItemsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(workItems);
    }
}