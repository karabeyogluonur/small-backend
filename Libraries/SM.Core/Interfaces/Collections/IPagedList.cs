using System;
namespace SM.Core.Interfaces.Collections
{
	public interface IPagedList<T>
	{
        int IndexFrom { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        List<T> Items { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }
}

