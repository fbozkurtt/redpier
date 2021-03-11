using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;
using System.Collections.Generic;

namespace Redpier.Application.DataTransferObjects
{
    public class RoleDto : IMapFrom<Role>
    {
        public string Name { get; set; }

        public IList<RoleClaimDto> RoleClaims { get; set; }
    }
}

