using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM.Core.DTOs.Blog;

namespace SM.Core.Features.Topics.SearchTopic
{
    public class SearchTopicResponse
    {
        public int IndexFrom { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public List<TopicDTO> Items { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

    }
}
