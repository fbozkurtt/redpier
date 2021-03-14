using System;
using System.ComponentModel.DataAnnotations;

namespace Redpier.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid? Id { get; set; }

        public DateTime Created { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedBy { get; set; }

    }
}