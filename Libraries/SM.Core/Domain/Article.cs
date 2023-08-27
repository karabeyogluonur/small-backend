using System;
namespace SM.Core.Domain
{
	public class Article
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public bool Deleted { get; set; }
		public bool Published { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual ICollection<Topic> Topics { get; set; }
	}
}

