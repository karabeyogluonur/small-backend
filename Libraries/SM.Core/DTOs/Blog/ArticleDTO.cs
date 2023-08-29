using System;
using SM.Core.Domain;
using SM.Core.DTOs.Membership;

namespace SM.Core.DTOs.Blog
{
	public class ArticleDTO
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ApplicationUserDTO Author { get; set; }
        public List<TopicDTO> Topics { get; set; }
    }
}

