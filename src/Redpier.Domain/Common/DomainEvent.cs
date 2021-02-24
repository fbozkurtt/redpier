using System;
using System.Collections.Generic;

namespace Redpier.Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccured = DateTimeOffset.UtcNow;
        }

        public bool IsPublished { get; set; }

        public DateTimeOffset DateOccured { get; set; }
    }
}
