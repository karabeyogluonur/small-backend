﻿using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services.Blog;
using SM.Infrastructre.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SM.Infrastructre.Services.Blog
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<Topic> _topicRepository;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _topicRepository = _unitOfWork.GetRepository<Topic>();
        }

        public async Task<IPagedList<Topic>> GetAllTopicsAsync(
            bool showDeactived = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool includeArticles = true)
        {
            IQueryable<Topic> topics = _topicRepository.GetAll();

            if(!showDeactived)
                topics = topics.Where(topic=>topic.Active);

            if (includeArticles)
                topics = topics.Include(topic => topic.Articles);

            return await topics.ToPagedListAsync(pageIndex:pageIndex, pageSize:pageSize);
        }

        public async Task<Topic> GetTopicByIdAsync(int topicId)
        {
            return await _topicRepository.GetFirstOrDefaultAsync(predicate:topic => topic.Id == topicId,disableTracking:false);
        }

        public async Task<Topic> GetTopicByNameAsync(string topicName)
        {
            return await _topicRepository.GetFirstOrDefaultAsync(predicate: topic => topic.Name.ToLower() == topicName.ToLower(),disableTracking:false);
        }

        public async Task InsertRangeTopicAsync(List<Topic> topics)
        {
            await _topicRepository.InsertAsync(topics);
        }

        public async Task InsertTopicAsync(Topic topic)
        {
            topic.CreatedDate = DateTime.Now;
            topic.Active = true;

            await _topicRepository.InsertAsync(topic);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IPagedList<Topic>> SearchTopicsAsync(
            string searchKeywords,
            bool showDeactived = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool includeArticles = true)
        {
            IQueryable<Topic> topics = _topicRepository.GetAll();

            if(!showDeactived)
                topics = topics.Where(topic => topic.Active);

            if (!String.IsNullOrEmpty(searchKeywords))
                topics = topics.Where(topic => topic.Name.Contains(searchKeywords));

            if (includeArticles)
                topics = topics.Include(topic => topic.Articles);

            return await topics.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            
        }

        public void UpdateTopic(Topic topic)
        {
            _topicRepository.Update(topic);
            _unitOfWork.SaveChanges();
        }

    }
}
