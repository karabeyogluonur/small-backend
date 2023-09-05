using System;
namespace SM.Core.Domain
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; }
        public bool HasReplies { get; set; }

        public int ArticleId { get; set; }
		public int AuthorId { get; set; }
		

		public ApplicationUser Author { get; set; }
		public ICollection<CommentReply> CommentReplies { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}

