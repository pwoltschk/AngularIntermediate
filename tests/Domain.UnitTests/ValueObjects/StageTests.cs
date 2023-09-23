using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.UnitTests.ValueObjects;

[TestClass]
public class StageTests
{
    [TestMethod]
    public void GivenValidId_WhenCreatingStage_ThenShouldReturnCorrectStage()
    {
        // Arrange
        var validStageId = 1;
        var expectedStageName = "In Progress";

        // Act
        var stage = Stage.FromId(validStageId);

        // Assert
        Assert.AreEqual(validStageId, stage.Id);
        Assert.AreEqual(expectedStageName, stage.Name);
    }

    [TestMethod]
    public void GivenInvalidId_WhenCreatingStage_ThenShouldThrowArgumentException()
    {
        // Arrange
        var invalidStageId = 99;

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => Stage.FromId(invalidStageId));
    }

    [TestMethod]
    public void GivenTwoStagesWithSameId_WhenComparing_ThenShouldBeEqual()
    {
        // Arrange
        var stage1 = Stage.FromId(1);
        var stage2 = Stage.FromId(1);

        // Act & Assert
        Assert.AreEqual(stage1, stage2);
    }

    [TestMethod]
    public void GivenTwoStagesWithDifferentIds_WhenComparing_ThenShouldNotBeEqual()
    {
        // Arrange
        var stage1 = Stage.FromId(1);
        var stage2 = Stage.FromId(2);

        // Act & Assert
        Assert.AreNotEqual(stage1, stage2);
    }

    [TestMethod]
    public void GivenAStage_WhenConvertingToString_ThenShouldReturnCorrectName()
    {
        // Arrange
        var stage = Stage.FromId(0);
        var expectedString = "Planned";

        // Act
        var result = stage.ToString();

        // Assert
        Assert.AreEqual(expectedString, result);
    }
}