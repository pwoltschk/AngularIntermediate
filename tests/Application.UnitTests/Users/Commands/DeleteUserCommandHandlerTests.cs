using Application.Common.Services;
using Application.Users.Command;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Users.Commands;

[TestClass]
public class DeleteUserCommandHandlerTests
{
    private Mock<IIdentityService> _identityServiceMock;
    private DeleteUserCommandHandler _handler;

    [TestInitialize]
    public void SetUp()
    {
        _identityServiceMock = new Mock<IIdentityService>();
        _handler = new DeleteUserCommandHandler(_identityServiceMock.Object);
    }

    [TestMethod]
    public async Task GivenDeleteUserCommand_WhenHandling_ThenShouldDeleteUser()
    {
        // Arrange
        var command = new DeleteUserCommand("1");

        // Act
        await ((IRequestHandler<DeleteUserCommand>)_handler)
            .Handle(command, CancellationToken.None);

        // Assert
        _identityServiceMock.Verify(s => s.DeleteUserAsync("1"), Times.Once);
    }
}