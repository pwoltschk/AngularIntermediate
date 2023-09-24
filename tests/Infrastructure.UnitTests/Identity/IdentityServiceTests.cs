using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.UnitTests.Identity;

[TestClass]
public class IdentityServiceTests
{
    private Mock<UserManager<IdentityUser>> _userManagerMock;
    private Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private IdentityService _identityService;

    [TestInitialize]
    public void SetUp()
    {
        _userManagerMock = MockUserManager<IdentityUser>();
        _roleManagerMock = MockRoleManager<IdentityRole>();
        _identityService = new IdentityService(_userManagerMock.Object, _roleManagerMock.Object);
    }

    [TestMethod]
    public async Task DeleteUserAsync_ShouldDeleteUser_WhenUserExists()
    {
        // Arrange
        var userId = "123";
        var user = new IdentityUser { Id = userId };
        _userManagerMock.Setup(u => u.FindByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        await _identityService.DeleteUserAsync(userId);

        // Assert
        _userManagerMock.Verify(u => u.DeleteAsync(user), Times.Once);
    }

    [TestMethod]
    public async Task GetUserAsync_ShouldReturnUserWithRolesAndPermissions()
    {
        // Arrange
        var userId = "1";
        var identityUser = new IdentityUser { Id = userId, UserName = "user1", Email = "user1@example.com" };
        var roles = new List<string> { "Admin" };
        var identityRole = new IdentityRole { Id = "2", Name = "Admin" };
        var claims = new List<Claim> { new Claim(nameof(Permission), "Read") };

        _userManagerMock.Setup(u => u.FindByIdAsync(userId))
            .ReturnsAsync(identityUser);
        _userManagerMock.Setup(u => u.GetRolesAsync(identityUser))
            .ReturnsAsync(roles);
        _roleManagerMock.Setup(r => r.FindByNameAsync("Admin"))
            .ReturnsAsync(identityRole);
        _roleManagerMock.Setup(r => r.GetClaimsAsync(identityRole))
            .ReturnsAsync(claims);

        // Act
        var result = await _identityService.GetUserAsync(userId);

        // Assert
        result.Roles.Should().ContainSingle()
            .Which.Permissions.Should().Contain("Read");
    }

    [TestMethod]
    public async Task CreateRoleAsync_ShouldCreateRole()
    {
        // Arrange
        var role = new Role("1", "Admin", new List<string> { "Read" });

        // Act
        await _identityService.CreateRoleAsync(role);

        // Assert
        _roleManagerMock.Verify(r => r.CreateAsync(It.Is<IdentityRole>(r => r.Name == "Admin")), Times.Once);
    }

    [TestMethod]
    public async Task UpdateRoleAsync_ShouldUpdateRoleWithNewPermissions()
    {
        // Arrange
        var role = new Role("1", "Admin", new List<string> { "Read", "Write" });
        var identityRole = new IdentityRole { Id = "1", Name = "Admin" };
        var claims = new List<Claim> { new Claim(nameof(Permission), "Read") };

        _roleManagerMock.Setup(r => r.FindByIdAsync(role.Id))
            .ReturnsAsync(identityRole);
        _roleManagerMock.Setup(r => r.GetClaimsAsync(identityRole))
            .ReturnsAsync(claims);

        // Act
        await _identityService.UpdateRoleAsync(role);

        // Assert
        _roleManagerMock.Verify(r => r.RemoveClaimAsync(identityRole, It.IsAny<Claim>()), Times.Once);
        _roleManagerMock.Verify(r => r.AddClaimAsync(identityRole, It.IsAny<Claim>()), Times.Exactly(2));
        _roleManagerMock.Verify(r => r.UpdateAsync(identityRole), Times.Once);
    }

    [TestMethod]
    public async Task DeleteRoleAsync_ShouldDeleteRole()
    {
        // Arrange
        var roleId = "1";
        var identityRole = new IdentityRole { Id = roleId };

        _roleManagerMock.Setup(r => r.FindByIdAsync(roleId))
            .ReturnsAsync(identityRole);

        // Act
        await _identityService.DeleteRoleAsync(roleId);

        // Assert
        _roleManagerMock.Verify(r => r.DeleteAsync(identityRole), Times.Once);
    }

    [TestMethod]
    public async Task UpdateUserAsync_ShouldUpdateUserWithRoles()
    {
        // Arrange
        var user = new User("1", "user1", new Email("user1@example.com"));
        var identityUser = new IdentityUser { Id = user.Id, UserName = user.Name, Email = user.Email.Value };
        var roles = new List<string> { "Admin" };

        _userManagerMock.Setup(u => u.FindByIdAsync(user.Id))
            .ReturnsAsync(identityUser);
        _userManagerMock.Setup(u => u.GetRolesAsync(identityUser))
            .ReturnsAsync(roles);

        // Act
        await _identityService.UpdateUserAsync(user);

        // Assert
        _userManagerMock.Verify(u => u.RemoveFromRolesAsync(identityUser, roles), Times.Once);
        _userManagerMock.Verify(u => u.AddToRoleAsync(identityUser, "Admin"), Times.Exactly(user.Roles.Count));
        _userManagerMock.Verify(u => u.UpdateAsync(identityUser), Times.Once);
    }



    [TestMethod]
    public async Task GetRolesAsync_ShouldReturnRolesWithPermissions()
    {
        // Arrange
        var roles = new List<IdentityRole> { new IdentityRole { Id = "1", Name = "Admin" } }.AsQueryable();
        var mockRoleQueryable = new Mock<IQueryable<IdentityRole>>();
        mockRoleQueryable.As<IAsyncEnumerable<IdentityRole>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<IdentityRole>(roles.GetEnumerator()));
        mockRoleQueryable.As<IQueryable<IdentityRole>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<IdentityRole>(roles.Provider));
        mockRoleQueryable.As<IQueryable<IdentityRole>>().Setup(m => m.Expression).Returns(roles.Expression);
        mockRoleQueryable.As<IQueryable<IdentityRole>>().Setup(m => m.ElementType).Returns(roles.ElementType);
        mockRoleQueryable.As<IQueryable<IdentityRole>>().Setup(m => m.GetEnumerator()).Returns(roles.GetEnumerator());

        _roleManagerMock.Setup(r => r.Roles).Returns(mockRoleQueryable.Object);

        var claims = new List<Claim> { new Claim(nameof(Permission), "Read") };
        _roleManagerMock.Setup(r => r.GetClaimsAsync(It.IsAny<IdentityRole>())).ReturnsAsync(claims);

        // Act
        var result = await _identityService.GetRolesAsync(CancellationToken.None);

        // Assert
        result.Should().ContainSingle()
            .Which.Permissions.Should().Contain("Read");
    }

    private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
    }

    private Mock<RoleManager<TRole>> MockRoleManager<TRole>() where TRole : class
    {
        var store = new Mock<IRoleStore<TRole>>();
        return new Mock<RoleManager<TRole>>(store.Object, null, null, null, null);
    }

    // Helper classes for async queryable
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public async ValueTask DisposeAsync()
        {
            _inner.Dispose();
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return _inner.MoveNext();
        }
    }

    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Execute<TResult>(expression);
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
        {
        }

        public TestAsyncEnumerable(Expression expression) : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAsyncEnumerator(cancellationToken);
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
    }
}