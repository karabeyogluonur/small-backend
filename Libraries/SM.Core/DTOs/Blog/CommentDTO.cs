using SM.Core.DTOs.Membership;

namespace SM.Core.DTOs.Blog
{
    public class CommentDTO
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public bool HasReplies { get; set; }
		public DateTime CreatedDate { get; set; }
		public ApplicationUserDTO Author { get; set; }
	}
}

