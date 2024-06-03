using ApiServer.Mapper;
using ApiServer.ViewModels;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Web.Tests.ApiServer.Mapper;

[TestClass]
public class UserMapperTests
{
    private UserMapper _mapper;

    [TestInitialize]
    public void SetUp()
    {
        _mapper = new UserMapper();
    }

    [TestMethod]
    public void MapUserToUserDto_ShouldMapCorrectly()
    {
        // Arrange
        List<Role> roles =
        [
            new("1", "Admin", (List<string>) []),
            new("2", "User", (List<string>) [])
        ];

        var user = new User("1", "John Doe", new Email("john@example.com"));

        user.Roles.AddRange(roles);
        // Act
        var result = _mapper.Map(user);

        // Assert
        result.Id.Should().Be(user.Id);
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email.Value);
        result.Roles.Should().BeEquivalentTo(user.Roles.Select(r => r.Name));
    }

    [TestMethod]
    public void MapUserDtoToUser_ShouldMapCorrectly()
    {
        // Arrange
        var userDto = new UserDto
        {
            Id = "1",
            Name = "John Doe",
            Email = "john@example.com",
            Roles = ["Admin", "User"]
        };

        // Act
        var result = _mapper.Map(userDto);

        // Assert
        result.Id.Should().Be(userDto.Id);
        result.Name.Should().Be(userDto.Name);
        result.Email.Value.Should().Be(userDto.Email);
        result.Roles.Select(r => r.Name).Should().BeEquivalentTo(userDto.Roles);
    }

    [TestMethod]
    public void MapUserDtoToUser_ShouldHandleEmptyRoles()
    {
        // Arrange
        var userDto = new UserDto
        {
            Id = "1",
            Name = "John Doe",
            Email = "john@example.com",
            Roles =  [] // Empty roles
        };

        // Act
        var result = _mapper.Map(userDto);

        // Assert
        result.Id.Should().Be(userDto.Id);
        result.Name.Should().Be(userDto.Name);
        result.Email.Value.Should().Be(userDto.Email);
        result.Roles.Should().BeEmpty();
    }
}