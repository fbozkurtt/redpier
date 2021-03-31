using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Redpier.Application.Common.Interfaces;
using Redpier.Shared.DTOs;
using Redpier.Shared.Mappings;
using Redpier.Shared.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Redpier.Application.Queries.Docker.System
{
    public class GetEndpointsQuery : IRequest<PaginatedListDto<DockerEndpointDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public bool All { get; set; } = true;
    }

    public class GetEndpointsQueryHandler : IRequestHandler<GetEndpointsQuery, PaginatedListDto<DockerEndpointDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetEndpointsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedListDto<DockerEndpointDto>> Handle(GetEndpointsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<PaginatedListDto<DockerEndpointDto>>(
                await _context.DockerEndpoints
                .ProjectTo<DockerEndpointDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.All ? request.PageSize : Int32.MaxValue));
        }
    }
}
