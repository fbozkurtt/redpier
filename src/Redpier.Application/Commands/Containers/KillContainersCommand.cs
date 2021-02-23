using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Containers
{
    class KillContainersCommand
    {
        public string[] ContainerIds { get; set; }

        public string[] ContainerNames { get; set; }
    }
}
