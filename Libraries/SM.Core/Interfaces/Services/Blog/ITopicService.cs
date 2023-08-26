using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Blog
{
    public interface ITopicService
    {
        Task<IPagedList<Topic>> GetAllTopicsAsync(bool showDeactived = false,int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IPagedList<Topic>> SearchTopicsAsync(string searchKeywords,bool showDeactived = false, int pageIndex = 0, int pageSize = int.MaxValue);
        Task InsertTopicAsync(Topic topic);
        void UpdateTopic(Topic topic);
        Task<Topic> GetTopicByIdAsync(int topicId);
    }
}
