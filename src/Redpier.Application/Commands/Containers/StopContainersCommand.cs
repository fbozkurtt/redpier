namespace Redpier.Application.Commands.Containers
{
    class StopContainersCommand
    {
        public string[] ContainerIds { get; set; }

        public string[] ContainerNames { get; set; }
    }
}
