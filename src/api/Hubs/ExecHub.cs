using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
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
        public string SendAsync(string id, string command, bool tty = true)
        {
            try
            {
                var cns = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                string stdout = string.Empty;

                var stream = _mediator.Send(
                    new StartContainerExecCommand()
                    { 
                        ExecId = id,
                        Tty = tty
                    }).GetAwaiter().GetResult();

                var bytes = Encoding.ASCII.GetBytes(command);

                stream.WriteAsync(bytes, 0, bytes.Length, cns.Token).GetAwaiter().GetResult();
                stream.CloseWrite();

                var output = stream.ReadOutputToEndAsync(cns.Token).GetAwaiter().GetResult();
                Console.WriteLine("output: " + output.stdout);
                stdout = output.stdout;

                stream.Dispose();

                return stdout;

            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return string.Empty;
            }
        }

        [HubMethodName("resize")]
        public async Task ResizeAsync(ResizeContainerExecTtyCommand command)
            => await _mediator.Send(command);
    }
}
