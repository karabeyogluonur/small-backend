using System;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;

namespace SM.Core.Interfaces.Services.Blog
{
	public interface IArticleService
	{
        Task<IPagedList<Article>> GetAllArticlesAsync(
            List<int> topicIds = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showNonPublished = false,
            bool includeTopics = true,
            bool includeAuthor = true
            );

        Task<IPagedList<Article>> SearchArticlesAsync(
            string searchKeywords,
            int pageIndex = 0,
            int pageSize = int.MaxValue
            );

        Task<Article> GetArticleByIdAsync(int articleId);

        Task DeleteAllTopicsAsync(int articleId);

        Task<int> InsertArticleAsync(Article article);

        void UpdateArticle(Article article);
    }
}

