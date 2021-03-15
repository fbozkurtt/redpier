using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Models;
using Redpier.Domain.Common;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly IPublisher _mediator;

        public DomainEventService(IPublisher mediator)
        {
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            Log.Information("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
