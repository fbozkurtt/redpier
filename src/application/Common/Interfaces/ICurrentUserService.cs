﻿namespace Redpier.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }

        public string Username { get; }
    }
}
