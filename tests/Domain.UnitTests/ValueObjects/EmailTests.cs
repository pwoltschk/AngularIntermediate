using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.UnitTests.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    public void GivenValidEmail_WhenCreatingEmail_ThenShouldReturnCorrectEmail()
    {
        // Arrange
        var validEmail = "test@example.com";
        var expectedEmail = "test@example.com";

        // Act
        var email = new Email(validEmail);

        // Assert
        Assert.AreEqual(expectedEmail, email.Value);
    }

    [TestMethod]
    public void GivenEmailWithUppercase_WhenCreatingEmail_ThenShouldBeNormalizedToLowercase()
    {
        // Arrange
        var emailWithUppercase = "Test@Example.Com";
        var expectedEmail = "test@example.com";

        // Act
        var email = new Email(emailWithUppercase);

        // Assert
        Assert.AreEqual(expectedEmail, email.Value);
    }

    [TestMethod]
    public void GivenEmptyEmail_WhenCreatingEmail_ThenShouldThrowArgumentException()
    {
        // Arrange
        var emptyEmail = "";

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => new Email(emptyEmail));
    }

    [TestMethod]
    public void GivenInvalidEmailFormat_WhenCreatingEmail_ThenShouldThrowArgumentException()
    {
        // Arrange
        var invalidEmail = "invalid-email";

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => new Email(invalidEmail));
    }

    [TestMethod]
    public void GivenTwoEmailsWithSameValue_WhenComparing_ThenShouldBeEqual()
    {
        // Arrange
        var email1 = new Email("test@example.com");
        var email2 = new Email("test@example.com");

        // Act & Assert
        Assert.AreEqual(email1, email2);
    }

    [TestMethod]
    public void GivenTwoEmailsWithDifferentValues_WhenComparing_ThenShouldNotBeEqual()
    {
        // Arrange
        var email1 = new Email("test1@example.com");
        var email2 = new Email("test2@example.com");

        // Act & Assert
        Assert.AreNotEqual(email1, email2);
    }

    [TestMethod]
    public void GivenAnEmail_WhenConvertingToString_ThenShouldReturnCorrectValue()
    {
        // Arrange
        var email = new Email("test@example.com");
        var expectedString = "test@example.com";

        // Act
        var result = email.ToString();

        // Assert
        Assert.AreEqual(expectedString, result);
    }
}