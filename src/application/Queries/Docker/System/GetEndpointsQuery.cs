using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Application.Common.Models;
using Redpier.Shared.DTOs;
using Redpier.Shared.Mappings;
using Redpier.Shared.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.System
{
    public class GetEndpointsQuery : IRequest<PaginatedList<DockerEndpointDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public bool All { get; set; } = false;
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
            return
                await _context.DockerEndpoints
                .OrderBy(w => w.Created)
                .ProjectTo<DockerEndpointDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.All ? Int32.MaxValue : request.PageSize);
        }
    }
}
