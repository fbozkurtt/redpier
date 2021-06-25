using Docker.DotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Exceptions;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Services
{
    public class DockerClientService : IDockerClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DockerClientService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IDockerClient CreateClient()
        {
            var endpointString = _httpContextAccessor.HttpContext.Request.Query["endpoint"].ToString();

            if (string.IsNullOrEmpty(endpointString))
                throw new ArgumentNullException("endpoint");

            var dockerEndpoint = Encoding.UTF8.GetString(
                Convert.FromBase64String(endpointString));

            if (dockerEndpoint == null)
                throw new NotFoundException("Endpoint", "Uri", dockerEndpoint);

            return new DockerClientConfiguration(
                        new Uri(dockerEndpoint))
                .CreateClient();
        }
    }
}
