using Microsoft.EntityFrameworkCore;
using Redpier.Shared.Mappings;
using Redpier.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Shared.DTOs
{
    public class PaginatedListDto<T> : IMapFrom<PaginatedList<T>>
    {
        public IList<T> Items { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        public int TotalCount { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

    }
}
