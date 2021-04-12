using Docker.DotNet;
using System.Threading.Tasks;

namespace Redpier.Application.Common.Interfaces
{
    public interface IDockerClientService
    {
        IDockerClient CreateClient();
    }
}
