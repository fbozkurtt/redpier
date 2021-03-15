using Redpier.Domain.Common;

namespace Redpier.Domain.Events
{
    public class EntityCreatedEvent : DomainEvent
    {
        public EntityCreatedEvent(object item)
        {
            Item = item;
        }

        public object Item { get; }
    }
}
