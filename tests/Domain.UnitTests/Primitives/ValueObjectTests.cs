using Domain.Primitives;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Domain.UnitTests.Primitives;
[TestClass]
public class ValueObjectTests
{
    [TestMethod]
    public void GivenSameValues_WhenUsingEqualOperator_ThenShouldBeEqual()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(5);

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void GivenDifferentValues_WhenUsingEqualOperator_ThenShouldNotBeEqual()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(10);

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GivenSameValues_WhenUsingNotEqualOperator_ThenShouldBeFalse()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(5);

        // Act
        var result = valueObject1 != valueObject2;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GivenDifferentValues_WhenUsingNotEqualOperator_ThenShouldBeTrue()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(10);

        // Act
        var result = valueObject1 != valueObject2;

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void GivenSameValues_WhenCallingEqualsMethod_ThenShouldReturnTrue()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(5);

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void GivenDifferentValues_WhenCallingEqualsMethod_ThenShouldReturnFalse()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(10);

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GivenTwoValueObjectsWithSameValues_WhenGettingHashCode_ThenShouldReturnSameHashCode()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(5);

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [TestMethod]
    public void GivenTwoValueObjectsWithDifferentValues_WhenGettingHashCode_ThenShouldReturnDifferentHashCode()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = new TestValueObject(10);

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [TestMethod]
    public void GivenNullValueObject_WhenUsingEqualOperator_ThenShouldReturnFalse()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        TestValueObject? valueObject2 = null;

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GivenNullValueObject_WhenUsingNotEqualOperator_ThenShouldReturnTrue()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        TestValueObject? valueObject2 = null;

        // Act
        var result = valueObject1 != valueObject2;

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public void GivenSameReference_WhenUsingEqualOperator_ThenShouldReturnTrue()
    {
        // Arrange
        var valueObject1 = new TestValueObject(5);
        var valueObject2 = valueObject1;

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeTrue();
    }
}

public class TestValueObject : ValueObject
{
    public int Value { get; }

    public TestValueObject(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

