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
        public async Task<string> SendAsync(string id, string command, bool tty = true)
        {
            try
            {
                string stdout = string.Empty;
                var stream = await _mediator.Send(
                    new StartContainerExecCommand()
                    { 
                        ExecId = id,
                        Tty = tty
                    });

                var bytes = Encoding.ASCII.GetBytes(command);
                await stream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None).ContinueWith(async (state) => {

                    var output = await stream.ReadOutputToEndAsync(CancellationToken.None);
                    Console.WriteLine(output.stdout + " " + output.stderr);
                    stdout = output.stdout;
                });
                stream.CloseWrite();

                //var (stdout, stderr) = await stream.ReadOutputToEndAsync(CancellationToken.None);


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
