using Docker.DotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Networks;
using Redpier.Application.Queries.Docker.Networks;
using Redpier.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class NetworkController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<NetworkResponse>> GetNetworks()
        {
            return await Mediator.Send(new ListNetworksQuery());
        }

        [HttpGet("[action]")]
        public async Task<IList<NetworkResponse>> GetNetworks([FromBody] ListNetworksQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<NetworkResponse> Inspect([FromQuery] InspectNetworkQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPost]
        public async Task<NetworksCreateResponse> Create(CreateNetworkCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Connect(ConnectNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpPut("[action]")]
        public async Task<ActionResult> Disconnect([FromQuery] DisconnectNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Roles = DefaultRoleNames.Admin)]
        [HttpDelete("[action]")]
        public async Task<NetworksPruneResponse> Prune(PruneNetworksCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
