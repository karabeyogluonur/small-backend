using SM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Blog
{
    public interface ITopicService
    {
        Task<List<Topic>> GetAllTopicsAsync(bool showDeactived = false);
        Task<List<Topic>> SearchTopicsAsync(string searchKeywords,bool showDeactived = false);
        Task InsertTopicAsync(Topic topic);
        void UpdateTopic(Topic topic);
        Task<Topic> GetTopicByIdAsync(int topicId);
    }
}
