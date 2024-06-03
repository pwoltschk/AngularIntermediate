using ApiServer.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;

namespace Web.Tests.ApiServer.Identity;

[TestClass]
public class UserContextTests
{
    private Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private UserContext _userContext;

    [TestInitialize]
    public void SetUp()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _userContext = new UserContext(_httpContextAccessorMock.Object);
    }

    [TestMethod]
    public void Constructor_ShouldThrowArgumentNullException_WhenHttpContextAccessorIsNull()
    {
        // Act
#pragma warning disable CA1806
        Action act = () => new UserContext(null);
#pragma warning restore CA1806

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'httpContextAccessor')");
    }

    [TestMethod]
    public void UserId_ShouldReturnUserIdFromClaims_WhenHttpContextIsAvailable()
    {
        // Arrange
        var userId = "12345";
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(ClaimTypes.NameIdentifier, userId)
        ]));

        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _userContext.UserId;

        // Assert
        result.Should().Be(userId);
    }

    [TestMethod]
    public void UserId_ShouldReturnNa_WhenHttpContextIsNull()
    {
        // Arrange
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        // Act
        var result = _userContext.UserId;

        // Assert
        result.Should().Be("n/a");
    }

    [TestMethod]
    public void UserId_ShouldReturnNa_WhenUserDoesNotHaveNameIdentifierClaim()
    {
        // Arrange
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());  // No claims
        var httpContext = new DefaultHttpContext { User = claimsPrincipal };
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        // Act
        var result = _userContext.UserId;

        // Assert
        result.Should().Be("n/a");
    }
}