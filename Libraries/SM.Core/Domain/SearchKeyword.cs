using System;
namespace SM.Core.Domain
{
	public class SearchKeyword
	{
		public int Id { get; set; }

		public int AuthorId { get; set; }
		public string Keyword { get; set; }

		public DateTime CreatedDate { get; set; }

		public ApplicationUser Author { get; set; }
	}
}

