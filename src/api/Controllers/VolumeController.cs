using Docker.DotNet.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Volumes;
using Redpier.Application.Queries.Docker.Volumes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class VolumeController : ApiControllerBase
    {
        [HttpGet]
        public async Task<VolumesListResponse> GetVolumes()
            => await Mediator.Send(new ListVolumesQuery());

        [HttpGet("[action]")]
        public async Task<VolumeResponse> Inspect([FromQuery] InspectVolumeQuery query)
            => await Mediator.Send(query);

        [HttpPost]
        public async Task<VolumeResponse> Create([FromQuery] CreateVolumeCommand command)
            => await Mediator.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Remove(RemoveVolumeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<VolumesPruneResponse> Prune(PruneVolumesCommand command)
            => await Mediator.Send(command);
    }
}
