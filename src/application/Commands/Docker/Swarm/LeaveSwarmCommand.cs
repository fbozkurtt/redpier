using System.ComponentModel.DataAnnotations;

namespace Redpier.Application.Commands.Docker.Swarm
{
    //TODO
    public class LeaveSwarmCommand
    {
        [Required]
        public string Endpoint { get; set; }

    }
}
