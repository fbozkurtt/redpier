using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Queries.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
{
    [Authorize]
    public class DockerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetContainers()
        {
            await Mediator.Send(new PingQuery());

            return NoContent();
        }
    }
}
