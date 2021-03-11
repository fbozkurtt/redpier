using Redpier.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redpier.Domain.Entities
{
    public class Role : BaseEntity, IHasDomainEvent
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        public virtual IList<User> Users { get; set; }

        //public virtual IList<UserRole> UserRoles { get; set; }
    }
}
