using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Redpier.Application.Commands.Docker.Exec;
using Redpier.Application.Common.Extensions;
using Redpier.Shared.Constants;
using Serilog;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
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
        private readonly IDockerClient _dockerClient;
        private static Dictionary<string, MultiplexedStream> _execSessions = new Dictionary<string, MultiplexedStream>();

        public ExecHub(ISender mediator, IDockerClient dockerClient)
        {
            _mediator = mediator;
            _dockerClient = dockerClient;
        }


        [HubMethodName("send")]
        public async Task<string> SendAsync(string execId, string command, bool tty = true)
        {
            try
            {
                MultiplexedStream stream;
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

                if (!_execSessions.ContainsKey(Context.ConnectionId))
                {
                    stream = await _mediator.Send(
                        new StartContainerExecCommand()
                        {
                            ExecId = execId,
                            Tty = tty
                        });

                    _execSessions.Add(Context.ConnectionId, stream);
                }
                else
                    stream = _execSessions[Context.ConnectionId];

                await stream.WriteAsync(command.ToArray(), cts.Token);
                var output = await stream.ReadAsync(cts.Token);
                Console.WriteLine(output);
                return output;
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

            if (_execSessions.ContainsKey(Context.ConnectionId))
                _execSessions.Remove(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
