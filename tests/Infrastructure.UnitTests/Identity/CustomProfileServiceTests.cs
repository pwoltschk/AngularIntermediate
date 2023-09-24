using Duende.IdentityServer.Models;
using FluentAssertions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.UnitTests.Identity
{
    [TestClass]
    public class CustomProfileServiceTests
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private CustomProfileService _profileService;

        [TestInitialize]
        public void SetUp()
        {
            _userManagerMock = MockUserManager<IdentityUser>();
            _roleManagerMock = MockRoleManager<IdentityRole>();
            _profileService = new CustomProfileService(_userManagerMock.Object, _roleManagerMock.Object);
        }

        [TestMethod]
        public async Task GetProfileDataAsync_ShouldReturnIssuedClaimsIncludingRoles()
        {
            // Arrange
            var user = new IdentityUser { Id = "123", UserName = "testuser" };
            var context = new ProfileDataRequestContext
            {
                Subject = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("sub", "123") }))
            };

            _userManagerMock.Setup(u => u.GetUserAsync(context.Subject))
                .ReturnsAsync(user);
            _userManagerMock.Setup(u => u.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "Admin" });

            var role = new IdentityRole("Admin");
            _roleManagerMock.Setup(r => r.FindByNameAsync("Admin"))
                .ReturnsAsync(role);
            _roleManagerMock.Setup(r => r.GetClaimsAsync(role))
                .ReturnsAsync(new List<Claim> { new Claim("role", "Admin") });

            // Act
            await _profileService.GetProfileDataAsync(context);

            // Assert
            context.IssuedClaims.Should().Contain(c => c.Type == "sub" && c.Value == user.Id);
            context.IssuedClaims.Should().Contain(c => c.Type == "name" && c.Value == user.UserName);
            context.IssuedClaims.Should().Contain(c => c.Type == "role" && c.Value == "Admin");
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
    }
}
