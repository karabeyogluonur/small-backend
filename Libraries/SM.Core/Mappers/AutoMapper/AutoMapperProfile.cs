﻿using AutoMapper;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.DTOs.Blog;
using SM.Core.DTOs.Membership;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Features.Articles.GetComment;
using SM.Core.Features.Articles.SearchArticle;
using SM.Core.Features.Articles.UpdateArticle;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.RefreshToken;
using SM.Core.Features.Auth.Register;
using SM.Core.Features.Comments.GetReply;
using SM.Core.Features.Comments.InsertReply;
using SM.Core.Features.Profiles.GetDraft;
using SM.Core.Features.Topics.GetAllTopic;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Features.Users.GetArticle;
using SM.Core.Features.Users.GetFollowed;
using SM.Core.Features.Users.GetFollower;
using SM.Core.Features.Users.LikeHistory;
using SM.Core.Interfaces.Collections;

namespace SM.Core.Mappers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Auth
            CreateMap<TokenDTO, LoginResponse>().ReverseMap();
            CreateMap<TokenDTO, RegisterResponse>().ReverseMap();
            CreateMap<RegisterRequest, ApplicationUser>().ReverseMap();
            CreateMap<TokenDTO, RefreshTokenResponse>().ReverseMap();
            #endregion

            #region Topic

            CreateMap<TopicDTO, Topic>().ReverseMap();

            CreateMap<IPagedList<Topic>, GetAllTopicResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<Topic>, SearchTopicResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();
            #endregion

            #region Membership

            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();

            #endregion

            #region Article
            

            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Topics, opt => opt.MapFrom(src => src.Topics))
                .ReverseMap();

            CreateMap<IPagedList<Article>, GetAllArticleResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<Article>, SearchArticleResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<Article, UpdateArticleRequest>().ForMember(article => article.ArticleId, opt => opt.Ignore()).ReverseMap();
            CreateMap<Article, UpdateArticleResponse>().ReverseMap();

            CreateMap<ArticleLike, ArticleLikeDTO>().ReverseMap();

            CreateMap<IPagedList<ArticleLike>, LikeHistoryResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items.Select(article=> article.Article))).ReverseMap();

            #endregion

            #region Comment

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();
            CreateMap<InsertReplyRequest, CommentReply>();

            CreateMap<CommentReply, CommentReplyDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();

            CreateMap<IPagedList<Comment>, GetCommentResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<CommentReply>, GetReplyResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();


            #endregion

            #region User


            CreateMap<Follow, FollowDTO>()
                .ForMember(dest => dest.Followee, opt => opt.MapFrom(src => src.Followee))
                .ForMember(dest => dest.Follower, opt => opt.MapFrom(src => src.Follower));

            CreateMap<IPagedList<Follow>, GetFollowerResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<Follow>, GetFollowedResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<Article>, GetArticleResponse>().ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<IPagedList<Article>, GetDraftResponse>().ForMember(dest => dest.Items,
               opt => opt.MapFrom(src => src.Items)).ReverseMap();

            CreateMap<SearchKeyword, SearchKeywordDTO>().ReverseMap();

            #endregion


        }
    }
}
