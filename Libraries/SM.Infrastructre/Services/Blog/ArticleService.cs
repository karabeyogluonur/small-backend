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
        private readonly IRepository<ArticleLike> _articleLikeRepository;
		public ArticleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_articleRepository = _unitOfWork.GetRepository<Article>();
            _articleLikeRepository = _unitOfWork.GetRepository<ArticleLike>();
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
            int userId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showNonPublished = false,
            bool includeTopics = true,
            bool includeAuthor = true
            )
        {
            IQueryable<Article> articles = _articleRepository.GetAll();

            if (topicIds != null)
                articles = articles.Where(article => article.Topics.Any(topic => topicIds.Contains(topic.Id)));

            if (userId > 0)
                articles = articles.Where(articles => articles.AuthorId == userId);

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

        public async Task<ArticleLike> GetArticleLikeAsync(int authorId, int articleId)
        {
            return await _articleLikeRepository.GetFirstOrDefaultAsync(predicate:articleLike => articleLike.ArticleId == articleId && articleLike.AuthorId == authorId);
        }

        public async Task<IPagedList<Article>> GetDraftsByUserIdAsync(int userId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            IQueryable<Article> articles = _articleRepository.GetAll(predicate:article=>article.AuthorId == userId && article.Published == false,
                                                                     include:inc=>inc.Include(article=>article.Author)
                                                                                     .Include(article=>article.Topics));

            return await articles.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
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

        public async Task InsertArticleLikeAsync(ArticleLike articleLike)
        {
            articleLike.CreatedDate = DateTime.Now;
            await _articleLikeRepository.InsertAsync(articleLike);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IPagedList<Article>> SearchArticlesAsync(
            string searchKeywords,
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            IQueryable<Article> articles = _articleRepository.GetAll(include:inc=>inc.Include(article=>article.Topics)
                                                                                     .Include(article=>article.Author)
                                                                                      );

            if (!String.IsNullOrEmpty(searchKeywords))
                articles = articles.Where(article => (
                                                        article.Title.ToLower().Contains(searchKeywords) ||
                                                        article.Topics.Any(topic => topic.Name.ToLower().Contains(searchKeywords.ToLower()))
                                                     ) &&
                                                     (
                                                        article.Deleted == false && article.Published == true
                                                     ) 
                                                     );

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

