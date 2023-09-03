using System;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services.Blog;
using SM.Infrastructre.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SM.Infrastructre.Services.Blog
{
	public class ArticleService : IArticleService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<Article> _articleRepository;
		public ArticleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_articleRepository = _unitOfWork.GetRepository<Article>();
		}

        public async Task DeleteAllTopicsAsync(int articleId)
        {
            Article article = await GetArticleByIdAsync(articleId);

            article.Topics.Clear();
            article.UpdatedDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();
           
        }

        public async Task<IPagedList<Article>> GetAllArticlesAsync(
            List<int> topicIds = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showNonPublished = false,
            bool includeTopics = true,
            bool includeAuthor = true)
        {
            IQueryable<Article> articles = _articleRepository.GetAll();

            if (topicIds != null)
                articles = articles.Where(article => article.Topics.Any(topic => topicIds.Contains(topic.Id)));

            if (!showNonPublished)
                articles = articles.Where(article => article.Published == true);

            if (includeTopics)
                articles = articles.Include(article => article.Topics);

            if (includeAuthor)
                articles = articles.Include(article => article.Author);

            return await articles.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            
        }



        public async Task<Article> GetArticleByIdAsync(int articleId)
        {
            return await _articleRepository.GetFirstOrDefaultAsync(
                predicate: article => article.Id == articleId,
                include:inc=>inc.Include(article=>article.Topics).Include(article=>article.Author),
                disableTracking:false);

        }

        public async Task<int> InsertArticleAsync(Article article)
        {
            article.CreatedDate = DateTime.Now;
            article.UpdatedDate = DateTime.Now;
            article.Published = false;
            article.Deleted = false;

            await _articleRepository.InsertAsync(article);
            await _unitOfWork.SaveChangesAsync();

            return article.Id;
            
        }

        public async Task<IPagedList<Article>> SearchArticlesAsync(
            string searchKeywords,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool includeTopics = true,
            bool includeAuthor = true)
        {
            IQueryable<Article> articles = _articleRepository.GetAll();

            if (!String.IsNullOrEmpty(searchKeywords))
                articles = articles.Where(article => article.Title.Contains(searchKeywords) || article.Content.Contains(searchKeywords));

            if (includeTopics)
                articles = articles.Include(article => article.Topics);

            if (includeAuthor)
                articles = articles.Include(article => article.Author);

            return await articles.ToPagedListAsync(pageIndex:pageIndex,pageSize:pageSize);
        }

        public void UpdateArticle(Article article)
        {
            article.UpdatedDate = DateTime.Now;

            _articleRepository.ChangeEntityState(article, EntityState.Modified);
            _articleRepository.Update(article);

            _unitOfWork.SaveChanges();
        }
    }
}

