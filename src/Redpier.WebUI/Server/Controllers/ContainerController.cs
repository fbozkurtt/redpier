using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Queries.Containers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
{
    [Authorize]
    public class ContainerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ContainerListResponse>>> ListContainers([FromQuery] ListContainersQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
