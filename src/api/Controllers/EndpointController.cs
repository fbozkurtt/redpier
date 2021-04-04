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
    public class EndpointController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<DockerEndpointDto>>> Get([FromQuery] GetEndpointsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateEndpointCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteEndpointCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> Ping(PingQuery query)
        {
            await Mediator.Send(query);

            return Ok();
        }
    }
}
