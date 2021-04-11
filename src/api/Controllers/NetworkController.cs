using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Networks;
using Redpier.Application.Queries.Docker.Networks;
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
        public async Task<NetworkResponse> Inspect(InspectNetworkQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<NetworksCreateResponse> Create(CreateNetworkCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Connect(ConnectNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> DisConnect(DisconnectNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteNetworkCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<NetworksPruneResponse> Prune(PruneNetworksCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
