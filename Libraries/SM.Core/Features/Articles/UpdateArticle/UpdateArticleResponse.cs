using System;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;

namespace SM.Core.Features.Articles.UpdateArticle
{
	public class UpdateArticleResponse
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
    }
}

