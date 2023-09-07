using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.UpdateTopic
{
	public class UpdateTopicHandler : IRequestHandler<UpdateTopicRequest,ApiResponse<UpdateTopicResponse>>
	{
        private readonly ITopicService _topicService;
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public UpdateTopicHandler(IArticleService articleService, ITopicService topicService, IMapper mapper, IWorkContext workContext)
        {
            _articleService = articleService;
            _topicService = topicService;
            _mapper = mapper;
            _workContext = workContext;
        }

        public async Task<ApiResponse<UpdateTopicResponse>> Handle(UpdateTopicRequest request, CancellationToken cancellationToken)
        {

            if (request.TopicNames.Count() == 0)
            {
                await _articleService.DeleteAllTopicsAsync(request.ArticleId);
                return ApiResponse<UpdateTopicResponse>.Successful(null, "Topics of the article have been updated");
            }
                

            Article article = await _articleService.GetArticleByIdAsync(request.ArticleId);

            if (article == null)
                return ApiResponse<UpdateTopicResponse>.Error(null, "Article is not found");

            if (article.AuthorId != await _workContext.GetAuthenticatedUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized for this article");

            List<Topic> newTopics = await PrepareNewTopicsAsync(request.TopicNames);

            if(newTopics.Any(article => article.Active == false))
                return ApiResponse<UpdateTopicResponse>.Error(newTopics.Where(topic=>topic.Active == false).ToDictionary(x => x.Name, x => x.Active), "Topic name is not avaible");

            if (newTopics.Any(topic => topic.Id == 0))
                await _topicService.InsertRangeTopicAsync(newTopics.FindAll(topic => topic.Id == 0).ToList());

            await _articleService.DeleteAllTopicsAsync(article.Id);

            article.Topics = newTopics;

            _articleService.UpdateArticle(article);

            UpdateTopicResponse response = new UpdateTopicResponse{ Topics = _mapper.Map<List<TopicDTO>>(article.Topics)};

            return ApiResponse<UpdateTopicResponse>.Successful(response, "Topics of the article have been updated");
            
        }

        public async Task<List<Topic>> PrepareNewTopicsAsync(List<string> topicNames)
        {
            List<Topic> topics = new List<Topic>();

            foreach (var topicName in topicNames)
            {
                Topic topic = await _topicService.GetTopicByNameAsync(topicName);

                if (topic != null)
                    topics.Add(topic);
                else
                    topics.Add(new Topic { Name = topicName, Active = true, CreatedDate = DateTime.Now });
            }

            return topics;
        }
    }
}

