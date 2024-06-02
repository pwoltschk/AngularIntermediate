using Application.Projects.Commands;
using Application.Projects.Requests;
using Domain.Entities;
using Domain.Primitives;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.Projects.Commands;

[TestClass]
public class CreateProjectCommandValidatorTests
{
    private Mock<IRepository<Project>> _repositoryMock;
    private CreateProjectCommandValidator _validator;

    [TestInitialize]
    public void SetUp()
    {
        _repositoryMock = new Mock<IRepository<Project>>();
        _validator = new CreateProjectCommandValidator(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task GivenDuplicateTitle_WhenValidating_ThenShouldFailValidation()
    {
        // Arrange
        var existingProjects = new List<Project> { new() { Title = "Existing Title" } };
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingProjects);

        var command = new CreateProjectCommand(new CreateProjectRequest { Title = "Existing Title" });

        // Act
        var result = await _validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Project.Title)
            .WithErrorMessage("The project title must be unique.")
            .WithErrorCode("ERR_UNIQUE_TITLE");
    }
}