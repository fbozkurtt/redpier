﻿using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Containers;
using Redpier.Application.Queries.Docker.Containers;
using Redpier.Shared.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class ContainerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ContainerListResponse>> Containers([FromQuery] ListContainersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<CreateContainerResponse> Create(CreateContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("[action]")]
        public async Task<string> Logs([FromQuery] GetContainerLogsQuery query)
        {
            var result = await Mediator.Send(query);
            StreamReader reader = new StreamReader(result);
            return reader.ReadToEnd();
        }

        //[HttpGet]
        //public async Task<Unit> Stats(GetContainerStatsQuery query)
        //{
        //    return await Mediator.Send(query);
        //}

        [HttpGet("[action]")]
        public async Task<ContainerInspectResponse> Inspect([FromQuery] InspectContainerQuery query)
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
        public async Task<ActionResult> Start([FromQuery] StartContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Stop([FromQuery] StopContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Restart([FromQuery] RestartContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Pause([FromQuery] PauseContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Unpause([FromQuery] UnpauseContainerCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Kill([FromQuery] KillContainerCommand command)
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
        [HttpPut]
        public async Task<ContainerUpdateResponse> Update(UpdateContainerCommand command)
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
        public async Task<ContainersPruneResponse> Prune([FromQuery] PruneContainersCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
