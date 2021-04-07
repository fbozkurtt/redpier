using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Containers;
using Redpier.Application.Queries.Docker.Containers;
using Redpier.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    [Authorize]
    public class ContainerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ContainerListResponse>> Containers([FromQuery] ListContainersQuery query)
        {
            return await Mediator.Send(query);
        }

        //[HttpGet]
        //public async Task<Unit> Logs(GetContainerLogsQuery query)
        //{
        //    return await Mediator.Send(query);
        //}

        //[HttpGet]
        //public async Task<Unit> Stats(GetContainerStatsQuery query)
        //{
        //    return await Mediator.Send(query);
        //}

        [HttpGet("[action]")]
        public async Task<ContainerInspectResponse> Inspect([FromQuery] InspectContainersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ContainerProcessesResponse> Processes(ListProcessesQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Start(StartContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Stop(StopContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Restart(RestartContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Pause(PauseContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Unpause(UnpauseContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Kill(KillContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Rename(RenameContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ContainerUpdateResponse> Update([FromBody] UpdateContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] RemoveContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpDelete("[action]")]
        public async Task<ContainersPruneResponse> Prune(PruneContainersCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
