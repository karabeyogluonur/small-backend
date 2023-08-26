using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Topics.SearchTopic
{
    public class SearchTopicRequest : IRequest<ApiResponse<SearchTopicResponse>>
    {
        public string SearchKeywords { get; set; }
        public bool ShowDeactived { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; } = (int)DefaultPageSize.SearchTopic;
    }
}
