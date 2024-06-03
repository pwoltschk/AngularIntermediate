using ApiServer.Filters;
using Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Web.Tests.ApiServer.Filters;

[TestClass]
public class ApiExceptionFilterAttributeTests
{
    private ApiExceptionFilterAttribute _filter;

    [TestInitialize]
    public void SetUp()
    {
        _filter = new ApiExceptionFilterAttribute();
    }

    [TestMethod]
    public void GivenValidationException_WhenOnExceptionInvoked_ShouldReturnBadRequestObjectResult()
    {
        // Arrange
        var exception = new ValidationException([new("Field1", "Error1")]);

        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = routeData,
            ActionDescriptor = actionDescriptor,
        };
        var context = new ExceptionContext(actionContext, (List<IFilterMetadata>) [])
        {
            Exception = exception
        };

        // Act
        _filter.OnException(context);

        // Assert
        context.ExceptionHandled.Should().BeTrue();
        context.Result.Should().BeOfType<BadRequestObjectResult>();

        var result = context.Result as BadRequestObjectResult;
        result!.Value.Should().BeOfType<ValidationProblemDetails>();

        var details = result.Value as ValidationProblemDetails;
        details!.Errors.Should().ContainKey("Field1");
        details.Errors["Field1"].Should().Contain("Error1");
    }

    [TestMethod]
    public void GivenNotFoundException_WhenOnExceptionInvoked_ShouldReturnNotFoundObjectResult()
    {
        // Arrange
        var exception = new NotFoundException("TestResource", 1);

        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = routeData,
            ActionDescriptor = actionDescriptor
        };
        var context = new ExceptionContext(actionContext, (List<IFilterMetadata>) [])
        {
            Exception = exception
        };

        // Act
        _filter.OnException(context);

        // Assert
        context.ExceptionHandled.Should().BeTrue();
        context.Result.Should().BeOfType<NotFoundObjectResult>();

        var result = context.Result as NotFoundObjectResult;
        result!.Value.Should().BeOfType<ProblemDetails>();

        var details = result.Value as ProblemDetails;
        details!.Title.Should().Be("The specified resource was not found.");
        details.Detail.Should().Be("Entity \"TestResource\" (1) was not found.");
    }

    [TestMethod]
    public void GivenGenericException_WhenOnExceptionInvoked_ShouldNotHandleException()
    {
        // Arrange
        var exception = new Exception("Generic exception");

        var httpContext = new DefaultHttpContext();
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = routeData,
            ActionDescriptor = actionDescriptor
        };
        var context = new ExceptionContext(actionContext, (List<IFilterMetadata>) [])
        {
            Exception = exception
        };

        // Act
        _filter.OnException(context);

        // Assert
        context.ExceptionHandled.Should().BeFalse();
        context.Result.Should().BeNull();
    }
}