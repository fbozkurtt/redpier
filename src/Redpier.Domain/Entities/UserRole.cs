using Redpier.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Domain.Entities
{
    public class UserRole : BaseEntity, IHasDomainEvent
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
