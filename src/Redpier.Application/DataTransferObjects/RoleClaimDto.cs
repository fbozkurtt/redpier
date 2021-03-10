using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.DataTransferObjects
{
    public class RoleClaimDto : IMapFrom<RoleClaim>
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
