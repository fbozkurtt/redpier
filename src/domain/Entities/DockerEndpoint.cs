using Redpier.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redpier.Domain.Entities
{
    public class DockerEndpoint : BaseEntity, IHasDomainEvent
    {
        [Required]
        [MaxLength(2048)]
        public string Uri { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
