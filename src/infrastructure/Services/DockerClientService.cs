using Docker.DotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Redpier.Application.Common.Exceptions;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Redpier.Infrastructure.Services
{
    public class DockerClientService : IDockerClientService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DockerClientService(IApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDockerClient> CreateClient()
        {
            var dockerEndpointId = _httpContextAccessor.HttpContext.Request.Query["endpoint"].ToString();

            if (String.IsNullOrEmpty(dockerEndpointId))
                throw new ArgumentNullException("endpoint");

            var dockerEndpoint = await _dbContext.DockerEndpoints.FirstAsync(w => w.Id == new Guid(dockerEndpointId));

            if (dockerEndpoint == null)
                throw new NotFoundException(nameof(DockerEndpoint), nameof(DockerEndpoint.Id), dockerEndpointId);

            return new DockerClientConfiguration(
                        new Uri(dockerEndpoint.Uri)
                        ).CreateClient();
        }
    }
}
