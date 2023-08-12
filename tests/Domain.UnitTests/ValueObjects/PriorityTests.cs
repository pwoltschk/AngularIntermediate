using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.ValueObjects;
using System;

namespace Domain.UnitTests.ValueObjects
{
    [TestClass]
    public class PriorityTests
    {
        [TestMethod]
        public void GivenValidName_WhenCreatingPriority_ThenShouldReturnCorrectPriority()
        {
            // Arrange
            var validPriorityName = "Medium";
            var expectedPriorityLevel = 2;

            // Act
            var priority = Priority.FromName(validPriorityName);

            // Assert
            Assert.AreEqual(expectedPriorityLevel, priority.Level);
            Assert.AreEqual(validPriorityName, priority.Name);
        }

        [TestMethod]
        public void GivenInvalidName_WhenCreatingPriority_ThenShouldThrowArgumentException()
        {
            // Arrange
            var invalidPriorityName = "Unknown";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => Priority.FromName(invalidPriorityName));
        }

        [TestMethod]
        public void GivenValidLevel_WhenCreatingPriority_ThenShouldReturnCorrectPriority()
        {
            // Arrange
            var validPriorityLevel = 3;
            var expectedPriorityName = "High";

            // Act
            var priority = Priority.FromLevel(validPriorityLevel);

            // Assert
            Assert.AreEqual(validPriorityLevel, priority.Level);
            Assert.AreEqual(expectedPriorityName, priority.Name);
        }

        [TestMethod]
        public void GivenInvalidLevel_WhenCreatingPriority_ThenShouldThrowArgumentException()
        {
            // Arrange
            var invalidPriorityLevel = 99;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => Priority.FromLevel(invalidPriorityLevel));
        }

        [TestMethod]
        public void GivenTwoPrioritiesWithSameLevel_WhenComparing_ThenShouldBeEqual()
        {
            // Arrange
            var priority1 = Priority.FromLevel(1); 
            var priority2 = Priority.FromLevel(1); 

            // Act & Assert
            Assert.AreEqual(priority1, priority2);
        }

        [TestMethod]
        public void GivenTwoPrioritiesWithDifferentLevels_WhenComparing_ThenShouldNotBeEqual()
        {
            // Arrange
            var priority1 = Priority.FromLevel(1); 
            var priority2 = Priority.FromLevel(2); 

            // Act & Assert
            Assert.AreNotEqual(priority1, priority2);
        }

        [TestMethod]
        public void GivenAPriority_WhenConvertingToString_ThenShouldReturnCorrectName()
        {
            // Arrange
            var priority = Priority.FromLevel(1);
            var expectedString = "Low";

            // Act
            var result = priority.ToString();

            // Assert
            Assert.AreEqual(expectedString, result);
        }
    }
}
