﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Identity;
using Redpier.Shared.Models;
using System;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<GetTokenResponse> Token(GetTokenCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<Guid> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("[action]")]
        public async Task<bool> Delete(DeleteUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
