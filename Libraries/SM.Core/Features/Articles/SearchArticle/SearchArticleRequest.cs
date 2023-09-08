using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Articles.SearchArticle
{
	public class SearchArticleRequest : IRequest<ApiResponse<SearchArticleResponse>>
	{
        public string SearchKeywords { get; set; }
        public bool ShowDeactived { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; } = (int)DefaultPageSize.SearchArticle;
    }
}

