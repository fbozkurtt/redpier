using Microsoft.EntityFrameworkCore;
using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Redpier.Application.Common.Mappings;
using Redpier.Application.Common.Models;
using Redpier.Application.DTOs;

namespace Redpier.Application.Queries.Docker.System
{
    public class GetEndpointsQuery : IRequest<PaginatedList<DockerEndpointDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetEndpointsQueryHandler : IRequestHandler<GetEndpointsQuery, PaginatedList<DockerEndpointDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEndpointsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DockerEndpointDto>> Handle(GetEndpointsQuery request, CancellationToken cancellationToken)
        {
            return await _context.DockerEndpoints
                .OrderBy(x => x.Created)
                .ProjectTo<DockerEndpointDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
