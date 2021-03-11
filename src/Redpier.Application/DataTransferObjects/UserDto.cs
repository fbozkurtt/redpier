using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;

namespace Redpier.Application.DataTransferObjects
{
    public class UserDto : IMapFrom<User>
    {
        public string Username { get; set; }

        public RoleDto Role { get; set; }
    }
}
