using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Redpier.Application.Commands.Docker.Exec;
using Redpier.Shared.Constants;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Web.API.Hubs
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class ExecHub : Hub
    {
        private readonly ISender _mediator;

        public ExecHub(ISender mediator)
        {
            _mediator = mediator;
        }

        [HubMethodName("send")]
        public async Task<(string, string)> SendAsync(string id, string message, bool tty = true)
        {
            var stream = await _mediator.Send(new StartContainerExecCommand() { ExecId = id, Tty = false });
            var bytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None);
            var output = await stream.ReadOutputToEndAsync(CancellationToken.None);
            Console.WriteLine(output.stdout + " " + output.stderr);
            return output;
        }

        [HubMethodName("resize")]
        public async Task ResizeAsync(ResizeContainerExecTtyCommand command)
            => await _mediator.Send(command);
    }
}
