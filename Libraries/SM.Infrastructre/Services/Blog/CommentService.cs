using System;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Repositores;
using SM.Core.Interfaces.Services.Blog;
using Microsoft.EntityFrameworkCore;
using SM.Infrastructre.Utilities.Extensions;
using SM.Core.Common.Enums.Blog;

namespace SM.Infrastructre.Services.Blog
{

    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<CommentReply> _commentReplyRepository;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _commentRepository = _unitOfWork.GetRepository<Comment>();
            _commentReplyRepository = _unitOfWork.GetRepository<CommentReply>();
        }


        #region Comment

        

        public void DeleteComment(Comment comment)
        {
            _commentRepository.Delete(comment);
            _unitOfWork.SaveChanges();
        }

        public async Task<IPagedList<Comment>> GetAllCommentsAsync(
            int articleId,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool includeReplies = true)
        {
            IQueryable<Comment> comments = _commentRepository.GetAll(predicate: comment=> comment.ArticleId == articleId,
                                                                     include:inc=>inc.Include(comment=>comment.Author));

            if (includeReplies)
                comments.Include(inc => inc.CommentReplies);

            return await comments.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _commentRepository.GetFirstOrDefaultAsync(predicate: comment => comment.Id == commentId,
                                                                   include: inc => inc.Include(comment => comment.CommentReplies).Include(comment=>comment.Author),
                                                                   disableTracking:false);
        }

        public async Task<int> InsertCommentAsync(Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            await _commentRepository.InsertAsync(comment);

            await _unitOfWork.SaveChangesAsync();

            return comment.Id;
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.Update(comment);
            _unitOfWork.SaveChanges();
        }

        public async Task CheckCommentRepliesAsync(int commentId, CommentReplyOperation operation)
        {
            Comment comment = await GetCommentByIdAsync(commentId);

            if (operation == CommentReplyOperation.Insert)
            {
                if(comment.HasReplies)
                    return;

                else
                    comment.HasReplies = true;
            }
            else if(operation == CommentReplyOperation.Delete)
            {
                if (comment.CommentReplies.Count() > 0)
                    comment.HasReplies = true;

                else
                    comment.HasReplies = false;
            }

            UpdateComment(comment);
               
        }

        #endregion

        #region CommentReply

        public void DeleteCommentReply(CommentReply commentReply)
        {
            _commentReplyRepository.Delete(commentReply);
            _unitOfWork.SaveChanges();
        }

        public async Task<IPagedList<CommentReply>> GetAllCommentRepliesAsync(int commentId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            IQueryable<CommentReply> commentReplies = _commentReplyRepository.GetAll(predicate: commentReply => commentReply.CommentId == commentId,
                                                                                     include:inc=>inc.Include(commentReply=>commentReply.Author));

            return await commentReplies.ToPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
        }

        public async Task<CommentReply> GetCommentReplyByIdAsync(int commentReplyId)
        {
            return await _commentReplyRepository.GetFirstOrDefaultAsync(predicate: commentReply => commentReply.Id == commentReplyId,
                                                                        include: inc => inc.Include(commentReply => commentReply.Author),
                                                                        disableTracking:false);
        }


        public async Task<int> InsertCommentReplyAsync(CommentReply commentReply)
        {
            commentReply.CreatedDate = DateTime.Now;
            await _commentReplyRepository.InsertAsync(commentReply);
            await _unitOfWork.SaveChangesAsync();

            return commentReply.Id;
        }



        public void UpdateCommentReply(CommentReply commentReply)
        {
            _commentReplyRepository.Update(commentReply);
        }

        #endregion

    }
}

