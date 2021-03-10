using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.DataTransferObjects
{
    public class RoleDto : IMapFrom<Role>
    {
        public string Name { get; set; }

        public IList<RoleClaimDto> RoleClaims { get; set; }
    }
}

