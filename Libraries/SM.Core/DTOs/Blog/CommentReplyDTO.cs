using System;
using SM.Core.DTOs.Membership;

namespace SM.Core.DTOs.Blog
{
	public class CommentReplyDTO
	{
		public int Id { get; set; }
		public int CommentId { get; set; }
		public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public ApplicationUserDTO Author { get; set; }
	}
}

