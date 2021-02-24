namespace Redpier.Application.Commands.Containers
{
    class RestartContainersCommand
    {
        public string[] ContainerIds { get; set; }

        public string[] ContainerNames { get; set; }
    }
}
