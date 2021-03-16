using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Common.Models;
using Redpier.Application.DTOs;
using Redpier.Application.Queries.Docker.System;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    [Authorize]
    public class DockerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Ping()
        {
            try
            {
                await Mediator.Send(new PingQuery());

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<DockerEndpointDto>>> Endpoints(GetEndpointsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
