using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    [Authorize]
    public class IdentityController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> Token(GetTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
