using System;
using SM.Core.DTOs.Blog;
using SM.Core.DTOs.Membership;

namespace SM.Core.Features.Users.LikeHistory
{
	public class LikeHistoryResponse
	{
        public int IndexFrom { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public List<ArticleDTO> Items { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
    }
}

