using System;
using SM.Core.Common.Enums.Blog;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;

namespace SM.Core.Interfaces.Services.Blog
{
	public interface ICommentService
	{
        #region Comment

        

        Task<IPagedList<Comment>> GetAllCommentsAsync(
            int articleId,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool includeReplies = true
            );

        Task<Comment> GetCommentByIdAsync(int commentId);

        void DeleteComment(Comment comment);

        Task<int> InsertCommentAsync(Comment comment);

        void UpdateComment(Comment comment);

        Task CheckCommentRepliesAsync(int commentId, CommentReplyOperation operation);

        #endregion


        #region Comment Reply

        Task<IPagedList<CommentReply>> GetAllCommentRepliesAsync(
            int commentId,
            int pageIndex = 0,
            int pageSize = int.MaxValue
            );


        Task<CommentReply> GetCommentReplyByIdAsync(int commentReplyId);

        void DeleteCommentReply(CommentReply commentReply);

        Task<int> InsertCommentReplyAsync(CommentReply commentReply);

        void UpdateCommentReply(CommentReply commentReply);

        #endregion
    }
}

