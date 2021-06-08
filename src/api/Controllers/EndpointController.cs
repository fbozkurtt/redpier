using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.System;
using Redpier.Application.DTOs;
using Redpier.Application.Queries.Docker.System;
using Redpier.Shared.Models;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
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
        public async Task<ActionResult> Ping([FromQuery] PingQuery query)
        {
            await Mediator.Send(query);

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<SystemInfoResponse> Info()
            => await Mediator.Send(new GetSystemInfoQuery());
    }
}
