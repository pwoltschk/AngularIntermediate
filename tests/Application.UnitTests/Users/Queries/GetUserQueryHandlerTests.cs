using Application.Common.Services;
using Application.Users.Queries;
using Domain.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Users.Queries;

[TestClass]
public class GetUserQueryHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private GetUserQueryHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new GetUserQueryHandler(_identityServiceMock.Object);
    }

    [TestMethod]
    public async Task GivenValidUserId_WhenHandlingGetUserQuery_ThenShouldReturnUser()
    {
        // Arrange
        var user = new User { Id = "1" };
        _identityServiceMock.Setup(s => s.GetUserAsync("1"))
            .ReturnsAsync(user);

        var query = new GetUserQuery("1");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().Be(user);
    }
}