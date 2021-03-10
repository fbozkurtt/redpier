using System;

namespace Redpier.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
    }
}
