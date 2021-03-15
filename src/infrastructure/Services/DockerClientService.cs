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

namespace Redpier.Infrastructure.Services
{
    public class DockerClientService : IDockerClientService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly HttpContext _httpContext;

        public DockerClientService(IApplicationDbContext dbContext, HttpContext httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public async Task<IDockerClient> CreateClient()
        {
            var dockerEndpointName = _httpContext.Request.Query["endpoint"].ToString();

            if (dockerEndpointName == null)
                return null;

            var dockerEndpoint = await _dbContext.DockerEndpoints.SingleOrDefaultAsync(w => w.Name.Equals(dockerEndpointName));

            return new DockerClientConfiguration(
                        new Uri(dockerEndpoint.Uri)
                        ).CreateClient();
        }
    }
}
