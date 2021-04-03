using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Common.Models;
using Redpier.Shared.DTOs;
using Redpier.Application.Queries.Docker.System;
using System.Threading.Tasks;
using Redpier.Shared.Models;
using Redpier.Application.Commands.Docker.System;

namespace Redpier.Web.API.Controllers
{
    [Authorize]
    public class SystemController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Ping(PingQuery query)
        {
            try
            {
                await Mediator.Send(query);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<DockerEndpointDto>>> Endpoints([FromQuery] GetEndpointsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateEndpoint(CreateEndpointCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteEndpoint([FromQuery] DeleteEndpointCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
