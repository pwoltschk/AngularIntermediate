using ApiServer.Mapper;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Web.Tests.ApiServer.Mapper;

[TestClass]
public class UserDetailsViewModelMapperTests
{
    private UserDetailsViewModelMapper _mapper;

    [TestInitialize]
    public void SetUp()
    {
        var roleMapper = new RoleMapper();
        var userMapper = new UserMapper();

        _mapper = new UserDetailsViewModelMapper(roleMapper, userMapper);
    }

    [TestMethod]
    public void MapUserToUserDetailsViewModel_ShouldMapCorrectly()
    {
        // Arrange
        var roles = new List<Role>
        {
            new("1", "Admin", new List<string>()), new("2", "User", new List<string>())
        };

        var user = new User("1", "John Doe", new Email("john@example.com"));

        user.Roles.AddRange(roles);

        // Act
        var result = _mapper.Map(user);

        // Assert
        result.User.Id.Should().Be(user.Id);
        result.User.Name.Should().Be(user.Name);
        result.User.Email.Should().Be(user.Email.Value);
        result.Roles.Select(r => r.Name).Should().BeEquivalentTo(user.Roles.Select(r => r.Name));
    }

    [TestMethod]
    public void MapUserToUserDetailsViewModel_ShouldHandleEmptyRoles()
    {
        // Arrange
        var user = new User("1", "John Doe", new Email("john@example.com"));

        // Act
        var result = _mapper.Map(user);

        // Assert
        result.User.Id.Should().Be(user.Id);
        result.User.Name.Should().Be(user.Name);
        result.User.Email.Should().Be(user.Email.Value);
        result.Roles.Should().BeEmpty();
    }
}