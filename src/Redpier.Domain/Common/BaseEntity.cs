using System;

namespace Redpier.Domain.Common
{
    public abstract class BaseEntity
    {
        public DateTime DateCreated { get; set; }

        public string UserCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public string UserModified { get; set; }
    }
}
