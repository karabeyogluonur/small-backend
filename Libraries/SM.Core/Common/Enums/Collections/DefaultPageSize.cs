﻿using System;
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

        #endregion

        #region Comment

        GetComment = 10,
        GetReply = 5

        #endregion
    }
}

