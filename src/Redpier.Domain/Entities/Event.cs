using Redpier.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redpier.Domain.Entities
{
    public class Event : BaseEntity, IHasDomainEvent
    {
        public string Type { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
