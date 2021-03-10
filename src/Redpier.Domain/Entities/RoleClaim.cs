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
    public class RoleClaim : BaseEntity, IHasDomainEvent
    {
        public Guid RoleId { get; set; }

        [Required]
        [MaxLength(64)]
        public string ClaimType { get; set; }

        [Required]
        [MaxLength(64)]
        public string ClaimValue { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
