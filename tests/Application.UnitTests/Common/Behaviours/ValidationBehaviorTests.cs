using Application.Common.Behaviours;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.UnitTests.Common.Behaviours;

[TestClass]
public class ValidationBehaviorTests
{
    [TestMethod]
    public async Task GivenNoValidators_WhenHandleIsCalled_ThenShouldCallNext()
    {
        // Arrange
        var validators = Enumerable.Empty<IValidator<TestRequest>>();
        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(n => n()).ReturnsAsync(new TestResponse());

        var behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, nextMock.Object);

        // Assert
        nextMock.Verify(n => n(), Times.Once); 
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GivenValidRequest_WhenHandleIsCalled_ThenShouldCallNext()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult()); 

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();
        nextMock.Setup(n => n()).ReturnsAsync(new TestResponse());

        var behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);

        // Act
        var result = await behavior.Handle(new TestRequest(), CancellationToken.None, nextMock.Object);

        // Assert
        nextMock.Verify(n => n(), Times.Once);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task GivenInvalidRequest_WhenHandleIsCalled_ThenShouldThrowValidationException()
    {
        // Arrange
        var validatorMock = new Mock<IValidator<TestRequest>>();
        var validationFailure = new ValidationFailure("Property", "Validation error");
        var validationResult = new ValidationResult(new List<ValidationFailure> { validationFailure });

        validatorMock
            .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var nextMock = new Mock<RequestHandlerDelegate<TestResponse>>();

        var behavior = new ValidationBehavior<TestRequest, TestResponse>(validators);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ValidationException>(async () =>
            await behavior.Handle(new TestRequest(), CancellationToken.None, nextMock.Object));

        nextMock.Verify(n => n(), Times.Never); 
    }
}

public class TestRequest : IRequest<TestResponse> { }

public class TestResponse { }