using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Queries.Containers;
using Redpier.Application.Commands.Containers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
{
    [Authorize]
    public class ContainerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<ContainerListResponse>> GetContainers()
        {
            return await Mediator.Send(new ListContainersQuery());
        }

        [HttpPost]
        public async Task<IList<ContainerListResponse>> GetContainers(ListContainersQuery query)
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
        public async Task<ContainerInspectResponse> Inspect(InspectContainersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ContainerProcessesResponse> Processes(ListProcessesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut("[action]")]
        public async Task<bool> Start(StartContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Stop(StopContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Restart(RestartContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Pause(PauseContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Unpause(UnpauseContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Kill(KillContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<bool> Rename(RenameContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ContainerUpdateResponse> Update(UpdateContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async Task<bool> Remove(RemoveContainerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async Task<ContainersPruneResponse> Prune(PruneContainersCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
