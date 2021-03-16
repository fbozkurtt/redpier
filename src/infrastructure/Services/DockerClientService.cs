using Docker.DotNet;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Redpier.Application.Common.Exceptions;

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
            var dockerEndpointName = _httpContextAccessor.HttpContext.Request.Query["endpoint"].ToString();

            if (String.IsNullOrEmpty(dockerEndpointName))
                throw new ArgumentNullException("endpoint");

            var dockerEndpoint = await _dbContext.DockerEndpoints.SingleOrDefaultAsync(w => w.Name.Equals(dockerEndpointName));

            if (dockerEndpoint == null)
                throw new NotFoundException(nameof(DockerEndpoint), nameof(DockerEndpoint.Name), dockerEndpointName);

            return new DockerClientConfiguration(
                        new Uri(dockerEndpoint.Uri)
                        ).CreateClient();
        }
    }
}
