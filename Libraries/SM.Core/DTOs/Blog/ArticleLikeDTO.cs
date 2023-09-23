using System;
using SM.Core.Domain;

namespace SM.Core.DTOs.Blog
{
	public class ArticleLikeDTO
	{
		public int AuthorId { get; set; }
		public int ArticleId { get; set; }

		public ArticleDTO Article { get; set; }
		public ApplicationUser Author { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

