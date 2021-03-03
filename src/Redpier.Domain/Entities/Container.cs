using Redpier.Domain.Common;
using System.Collections.Generic;

namespace Redpier.Domain.Entities
{
    public class Container : BaseEntity, IHasDomainEvent
    {
        public string Id { get; set; }

        public string ShortId { get; set; }

        public string[] Names { get; set; }

        //public string Command { get; set; }

        public Image Image { get; set; }
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
