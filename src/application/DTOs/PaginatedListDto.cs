using System.Collections.Generic;

namespace Redpier.Application.DTOs
{
    public class PaginatedListDto<T>
    {
        public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

    }
}
