﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Mappings;
using Redpier.Domain.Entities;
using Redpier.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Commands.Docker.System
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class CreateEndpointCommand : IRequest, IMapFrom<DockerEndpoint>
    {
        [Required]
        [MaxLength(2048)]
        public string Uri { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }

    public class CreateEndpointCommandHandler : IRequestHandler<CreateEndpointCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateEndpointCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateEndpointCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.DockerEndpoints.AddAsync(_mapper.Map<DockerEndpoint>(request));
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
