using System;
namespace SM.Core.DTOs.Membership
{
	public class FollowDTO
	{

		public int FolloweeId { get; set; }
		public int FollowerId { get; set; }
		public ApplicationUserDTO Followee { get; set; }
        public ApplicationUserDTO Follower { get; set; }
    }
}

