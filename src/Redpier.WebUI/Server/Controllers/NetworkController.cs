using Docker.DotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Networks;
using Redpier.Application.Queries.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.WebUI.Server.Controllers
{
    public class NetworkController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IList<NetworkResponse>> GetNetworks(ListNetworksQuery query)
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

        [HttpPut]
        public async Task<bool> Connect(ConnectNetworkCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<bool> DisConnect(DisconnectNetworkCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<bool> Delete(DeleteNetworkCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<NetworksPruneResponse> Prune(PruneNetworksCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
