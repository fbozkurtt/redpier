using Redpier.Domain.Common;
using Redpier.Domain.Entities;

namespace Redpier.Domain.Events
{
    public class ContainersStartedEvent : DomainEvent
    {
        public ContainersStartedEvent(Container[] items)
        {
            Items = items;
        }

        public Container[] Items { get; }
    }
}
