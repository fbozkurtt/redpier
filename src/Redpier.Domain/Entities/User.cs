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
    public class User : BaseEntity, IHasDomainEvent
    {
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime LockoutEnd { get; set; }

        public DateTime? LastLogin { get; set; }

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
