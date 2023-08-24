using Microsoft.EntityFrameworkCore;
using SM.Core.Domain;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services.Blog;

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

        public async Task<List<Topic>> GetAllTopicsAsync(bool showDeactived = false)
        {
            IQueryable<Topic> topics = _topicRepository.GetAll();

            if(!showDeactived)
                topics = topics.Where(topic=>topic.Active);

            return await topics.ToListAsync();
        }

        public async Task<Topic> GetTopicByIdAsync(int topicId)
        {
            return await _topicRepository.GetFirstOrDefaultAsync(predicate:topic => topic.Id == topicId);
        }

        public async Task InsertTopicAsync(Topic topic)
        {
            topic.CreatedDate = DateTime.Now;
            topic.Active = true;

            await _topicRepository.InsertAsync(topic);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Topic>> SearchTopicsAsync(string searchKeywords,bool showDeactived = false)
        {
            IQueryable<Topic> topics = _topicRepository.GetAll();

            if(!showDeactived)
                topics = topics.Where(topic => topic.Active);

            if (!String.IsNullOrEmpty(searchKeywords))
                topics = topics.Where(topic => topic.Name.Contains(searchKeywords));

            return await topics.ToListAsync();
            
        }

        public void UpdateTopic(Topic topic)
        {
            _topicRepository.Update(topic);
            _unitOfWork.SaveChanges();
        }
    }
}
