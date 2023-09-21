using System;
using SM.Core.Common.Enums.Collections;
using SM.Core.DTOs.Blog;

namespace SM.Core.Features.Users.GetArticle
{
	public class GetArticleResponse
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

