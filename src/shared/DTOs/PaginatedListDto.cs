using Microsoft.EntityFrameworkCore;
using Redpier.Shared.Mappings;
using Redpier.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redpier.Shared.DTOs
{
    public class PaginatedListDto<T>
    {
        public IList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

    }
}
