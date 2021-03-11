using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;

namespace Redpier.Application.DataTransferObjects
{
    public class RoleClaimDto : IMapFrom<RoleClaim>
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
