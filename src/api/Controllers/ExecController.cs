using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redpier.Application.Commands.Docker.Exec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Web.API.Controllers
{
    public class ExecController : ApiControllerBase
    {
        [HttpPost]
        public async Task<string> Create(CreateExecCommand command)
            => await Mediator.Send(command);

        [HttpPut("[action]")]
        public async Task<ActionResult> Resize([FromQuery] ResizeContainerExecTtyCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
