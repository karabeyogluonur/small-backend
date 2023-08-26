using System;
using Microsoft.EntityFrameworkCore;
using SM.Core.Interfaces.Collections;

namespace SM.Infrastructre.Persistence.Collections
{
    public class PagedList<T> : IPagedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int IndexFrom { get; set; }
        public List<T> Items { get; set; }
        public bool HasPreviousPage => PageIndex - IndexFrom > 0;
        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;
    }
}

