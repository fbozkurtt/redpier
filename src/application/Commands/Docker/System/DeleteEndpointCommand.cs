using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Application.Common.Exceptions;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using Redpier.Shared.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.System
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class DeleteEndpointCommand : IRequest//, IMapFrom<DockerEndpoint>
    {
        public Guid Id { get; set; }
    }

    public class DeleteEndpointCommandHandler : IRequestHandler<DeleteEndpointCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteEndpointCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteEndpointCommand request, CancellationToken cancellationToken)
        {
            var endpoint = _dbContext.DockerEndpoints.Find(request.Id);

            if (endpoint == null)
                throw new NotFoundException(nameof(DockerEndpoint), nameof(DockerEndpoint.Id), request.Id);

            _dbContext.DockerEndpoints.Remove(endpoint);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
