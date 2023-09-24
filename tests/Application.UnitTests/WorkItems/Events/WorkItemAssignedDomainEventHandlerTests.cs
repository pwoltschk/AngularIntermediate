using Application.WorkItems.Events;
using Application.Common.Services;
using Domain.Entities;
using Domain.Events;
using Domain.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.WorkItems.Events
{
    [TestClass]
    public class WorkItemAssignedDomainEventHandlerTests
    {
        private Mock<IEmailService> _emailServiceMock;
        private Mock<IRepository<WorkItem>> _repositoryMock;
        private WorkItemAssignedDomainEventHandler _handler;

        [TestInitialize]
        public void SetUp()
        {
            _emailServiceMock = new Mock<IEmailService>();
            _repositoryMock = new Mock<IRepository<WorkItem>>();
            _handler = new WorkItemAssignedDomainEventHandler(_emailServiceMock.Object, _repositoryMock.Object);
        }

        [TestMethod]
        public async Task GivenValidWorkItem_WhenHandlingDomainEvent_ThenShouldSendEmail()
        {
            // Arrange
            var workItem = new WorkItem { Id = 1, AssignedTo = "test@example.com" };
            var domainEvent = new WorkItemAssignedDomainEvent(workItem.Id);

            _repositoryMock.Setup(r => r.GetByIdAsync(workItem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workItem);

            // Act
            await _handler.Handle(domainEvent, CancellationToken.None);

            // Assert
            _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), workItem.AssignedTo), Times.Once);
        }

        [TestMethod]
        public async Task GivenInvalidWorkItem_WhenHandlingDomainEvent_ThenShouldNotSendEmail()
        {
            // Arrange
            var invalidWorkItemId = 99;
            var domainEvent = new WorkItemAssignedDomainEvent(invalidWorkItemId);

            _repositoryMock.Setup(r => r.GetByIdAsync(invalidWorkItemId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkItem)null);

            // Act
            await _handler.Handle(domainEvent, CancellationToken.None);

            // Assert
            _emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
