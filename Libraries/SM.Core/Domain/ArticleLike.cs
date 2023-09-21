using System;
namespace SM.Core.Domain
{
	public class ArticleLike
	{
		public int AuthorId { get; set; }
		public int ArticleId { get; set; }
		public Article Article { get; set; }
		public ApplicationUser Author { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

