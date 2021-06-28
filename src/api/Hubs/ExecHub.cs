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
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
                var cancellationTaskSource = new TaskCompletionSource<object?>();
                string stdout = string.Empty;

                var stream = _mediator.Send(
                    new StartContainerExecCommand()
                    {
                        ExecId = id,
                        Tty = tty
                    }).GetAwaiter().GetResult();

                var bytes = Encoding.ASCII.GetBytes(command);

                await stream.WriteAsync(bytes, 0, bytes.Length, cts.Token);
                stream.CloseWrite();


                var result = await Task.WhenAny(stream.ReadOutputToEndAsync(cts.Token), cancellationTaskSource.Task);
                stream.Dispose();
                if (result == cancellationTaskSource.Task)
                    return default;
                else
                {
                    var output = await (Task<(string, string)>)result;
                    return output.Item1;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            return default;
        }

        [HubMethodName("resize")]
        public async Task ResizeAsync(ResizeContainerExecTtyCommand command)
            => await _mediator.Send(command);

        public override Task OnConnectedAsync()
        {
            Log.Information($"New Docker Exec connection established by the user \"{Context.User.Identity.Name}\"");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (exception == null)
                Log.Information($"Docker Exec connection aborted by the user \"{Context.User.Identity.Name}\"");
            else
                Log.Error($"An error occured during Docker Exec connection of the user \"{Context.User.Identity.Name}\":\n{exception.Message}");

            return base.OnDisconnectedAsync(exception);
        }
    }
}
