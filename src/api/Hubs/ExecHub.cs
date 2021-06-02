using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Redpier.Application.Commands.Docker.Exec;
using Redpier.Shared.Constants;
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

        private MultiplexedStream _stream;

        public ExecHub(ISender mediator)
        {
            _mediator = mediator;
        }

        [HubMethodName("start")]
        public async Task StartAsync(StartContainerExecCommand command)
        {
            _stream = await _mediator.Send(command);
        }

        [HubMethodName("send")]
        public async Task SendAsync(string message)
        {
            var bytes = Encoding.ASCII.GetBytes(message);
            await _stream.WriteAsync(bytes, 0, bytes.Length, CancellationToken.None);
        }

        [HubMethodName("receive")]
        public async Task<(string, string)> ReceiveAsync()
            => await _stream.ReadOutputToEndAsync(CancellationToken.None);

        [HubMethodName("resize")]
        public async Task ResizeAsync(ResizeContainerExecTtyCommand command)
            => await _mediator.Send(command);

        public override Task OnDisconnectedAsync(Exception ex)
        {
            _stream.CloseWrite();
            _stream.Dispose();

            return base.OnDisconnectedAsync(ex);
        }
    }
}
