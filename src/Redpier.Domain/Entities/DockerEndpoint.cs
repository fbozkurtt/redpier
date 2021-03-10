using Redpier.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Domain.Entities
{
    public class DockerEndpoint : BaseEntity, IHasDomainEvent
    {
        [Required]
        [MaxLength(2048)]
        public string Uri { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
