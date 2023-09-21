using System;
namespace SM.Core.Common.Enums.Collections
{
	public enum DefaultPageSize
	{
        #region Topic

        GetAllTopic = 10,
        SearchTopic = 3,

        #endregion

        #region Article

        GetAllArticle = 10,
        SearchArticle = 5,

        #endregion

        #region Comment

        GetComment = 10,
        GetReply = 5,

        #endregion

        #region User

        GetFollower = 10,
        GetFollewed = 10,
        GetArticle = 5

        #endregion
    }
}

