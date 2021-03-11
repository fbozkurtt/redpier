using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Queries.Docker.System;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
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
