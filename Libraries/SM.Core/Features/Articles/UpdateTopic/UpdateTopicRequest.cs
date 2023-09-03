using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Articles.UpdateTopic
{
	public class UpdateTopicRequest : IRequest<ApiResponse<UpdateTopicResponse>>
	{
		public int ArticleId { get; set; }
		public List<string> TopicNames { get; set; } = new List<string>();
	}
}

