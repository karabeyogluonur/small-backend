using System;
namespace SM.Core.Domain
{
	public class CommentReply
	{
		public int Id { get; set; }
		public string Content { get; set; }

		public int CommentId { get; set; }
		public int AuthorId { get; set; }

		public ApplicationUser Author { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

