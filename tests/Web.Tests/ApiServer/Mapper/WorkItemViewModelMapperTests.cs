using ApiServer.Mapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Web.Tests.ApiServer.Mapper;

[TestClass]
public class WorkItemViewModelMapperTests
{
    private WorkItemViewModelMapper _mapper;

    [TestInitialize]
    public void SetUp()
    {
        var workItemMapper = new WorkItemMapper();

        _mapper = new WorkItemViewModelMapper(workItemMapper);
    }

    [TestMethod]
    public void MapWorkItemsToWorkItemsViewModel_ShouldMapCorrectly()
    {
        // Arrange
        var workItems = new List<WorkItem>
        {
            new() { Id = 1, Title = "Work Item 1" },
            new() { Id = 2, Title = "Work Item 2" }
        };

        // Act
        var result = _mapper.Map(workItems);

        // Assert
        result.WorkItems.Should().HaveCount(2);
        result.WorkItems[0].Id.Should().Be(workItems[0].Id);
        result.WorkItems[0].Title.Should().Be(workItems[0].Title);
        result.WorkItems[1].Id.Should().Be(workItems[1].Id);
        result.WorkItems[1].Title.Should().Be(workItems[1].Title);
    }

    [TestMethod]
    public void MapWorkItemsToWorkItemsViewModel_ShouldHandleEmptyList()
    {
        // Arrange
        IEnumerable<WorkItem> workItems = [];

        // Act
        var result = _mapper.Map(workItems);

        // Assert
        result.WorkItems.Should().BeEmpty();
    }
}