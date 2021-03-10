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
    public class Role : BaseEntity, IHasDomainEvent
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
