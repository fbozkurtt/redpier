using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Models;
using Redpier.Domain.Entities;
using Redpier.Domain.Events;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.EventHandlers
{
    public class EntityCreatedEventHandler : INotificationHandler<DomainEventNotification<EntityCreatedEvent>>
    {
        private readonly IApplicationDbContext _dbContext;

        public EntityCreatedEventHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DomainEventNotification<EntityCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            Log.Information("Event occured: {DomainEvent}", domainEvent.GetType().Name);

            await _dbContext.Events.AddAsync(new Event() { Type = domainEvent.GetType().Name });

            await _dbContext.SaveChangesAsync();
        }
    }
}
