using Docker.DotNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Redpier.Application.Commands.Docker.Exec;
using Redpier.Shared.Constants;
using Redpier.Web.API.Utils;
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
            //try
            //{
            //    string stdout = string.Empty;

            //    var stream =  _mediator.Send(
            //        new StartContainerExecCommand()
            //        {
            //            ExecId = id,
            //            Tty = tty
            //        }).GetAwaiter().GetResult();

            //    if(stream == null)
            //        Console.WriteLine("stream is null");
            //    var bytes = Encoding.ASCII.GetBytes(command);
            //    stream.Write(bytes, 0, bytes.Length);
            //    stream.CloseWrite();

            //    var output = stream.ReadOutputToEnd().stdout;

            //    Console.WriteLine("output: " + output);

            //    //stdout = output;
            //    stream.Dispose();

            //    return output;

            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex.Message);
            //    return string.Empty;
            //}
            return null;
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
